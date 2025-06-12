using AituConnectApi.Dto.Requests;
using AituConnectApi.Extensions;
using AituConnectApi.Models;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var userId = this.GetUserId();

            var user = await _cacheService.GetAsync<User>(userId);

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

    }
}
