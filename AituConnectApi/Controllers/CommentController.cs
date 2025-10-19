using AituConnectApi.Dto.Requests;
using AituConnectApi.Extensions;
using AituConnectApi.Models;
using AituConnectApi.Producers.Abstractions;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly IMessageProducer _producer;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IPostService postService, IMessageProducer producer, IUserService userService)
        {
            _commentService = commentService;
            _postService = postService;
            _producer = producer;
            _userService = userService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Content) || string.IsNullOrWhiteSpace(dto.PostId))
            {
                return BadRequest("Invalid comment data.");
            }

            var userId = this.GetUserId();
            var user = await _userService.GetByIdAsync(userId);
            var username = user?.UserName;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var post = await _postService.GetByIdAsync(dto.PostId);
            var postOwner = post?.User;

            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                PostId = dto.PostId,
                UserId = userId,
                ParentId = dto.ParentCommentId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            if (postOwner != null)
            {
                var payload = new Contracts.CreateCommentContract
                {
                    CommentId = comment.Id,
                    UserId = userId,
                    PostId = dto.PostId,
                    Content = dto.Content,
                    OwnerEmail = postOwner.Email,
                    UserName = username ?? "Unknown",
                    CreatedAt = comment.CreatedAt
                };

                await _producer.PublishAsync(payload);
            }

            await _commentService.AddAsync(comment);

            return Ok();
        }

        [HttpPost("reply")]
        [Authorize]
        public async Task<IActionResult> ReplyToComment([FromBody] ReplyCommentRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Content) || string.IsNullOrWhiteSpace(dto.PostId) || string.IsNullOrWhiteSpace(dto.ParentCommentId))
            {
                return BadRequest("Invalid reply data.");
            }

            var userId = this.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var reply = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                PostId = dto.PostId,
                UserId = userId,
                ParentId = dto.ParentCommentId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _commentService.AddAsync(reply);

            return Ok();
        }
    }
}
