using AituConnectApi.Dto.Requests;
using AituConnectApi.Extensions;
using AituConnectApi.Models;
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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                PostId = dto.PostId,
                UserId = userId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

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
