using AituConnectApi.Dto.Requests;
using AituConnectApi.Dto.Responses;
using AituConnectApi.Extensions;
using AituConnectApi.Models;
using AituConnectApi.Models.Redis;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICacheService _cacheService;
        private readonly ISubjectService _subjectService;

        public PostController(IPostService postService, ICacheService cacheService, ISubjectService subjectService)
        {
            _postService = postService;
            _cacheService = cacheService;
            _subjectService = subjectService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var userId = this.GetUserId();

            var user = await _cacheService.GetAsync<UserCache>(userId);

            var subjects = await _subjectService.GetSubjectsByIds(dto.Subjects);

            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = userId,
                Title = dto.Title,
                Content = dto.Content,
                UniversityId = user.UniversityId,
                Subjects = subjects,
                CreatedAt = DateTime.UtcNow
            };

            await _postService.AddAsync(post);

            return Ok();
        }

        [Authorize]
        [HttpGet("get-all-by-university")]
        public async Task<IActionResult> GetAllPostsByUniversity()
        {
            var userId = this.GetUserId();

            var user = await _cacheService.GetAsync<UserCache>(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var posts = await _postService.GetAllByUniversity(user.UniversityId);

            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            var dto = posts.Select(p => new PostDetailsResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Subjects = p.Subjects.Select(s => s.Name).ToList(),
                OwnerName = p.User?.UserName ?? "Unknown"
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            // Build tree
            var commentLookup = post.Comments
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    Username = c.User?.UserName ?? "Unknown"
                })
                .ToDictionary(c => c.Id);

            foreach (var comment in post.Comments)
            {
                if (comment.ParentId != null && commentLookup.ContainsKey(comment.ParentId))
                {
                    commentLookup[comment.ParentId].Replies.Add(commentLookup[comment.Id]);
                }
            }

            // Take only root comments (without parent)
            var rootComments = commentLookup.Values
                .Where(c => !post.Comments.Any(pc => pc.Id == c.Id && pc.ParentId != null))
                .ToList();

            var dto = new PostDetailsResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Subjects = post.Subjects.Select(s => s.Name).ToList(),
                Comments = rootComments,
                OwnerName = post.User?.UserName ?? "Unknown"
            };

            return Ok(dto);
        }
    }
}
