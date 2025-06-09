//using AituConnectApi.Dto.Requests;
//using AituConnectApi.Services.Abstractions;
//using AituConnectApi.Services.Implementations;
//using Microsoft.AspNetCore.Mvc;

//namespace AituConnectApi.Controllers
//{
//    [ApiController]
//    [Route("api/posts")]
//    public class PostController : ControllerBase
//    {
//        private readonly IPostService _postService;

//        public PostController(IPostService postService)
//        {
//            _postService = postService;
//        }

//        [HttpPost("add")]
//        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto dto)
//        {
//            if (dto == null)
//            {
//                return BadRequest();
//            }

//            var user = new Models.User
//            {
//                Id = Guid.NewGuid().ToString(),
//                UserName = dto.UserName,
//                Email = dto.Email,
//                PasswordHash = ComputeSha256Hash(dto.Password),
//                UniversityId = dto.UniversityId,
//                MajorId = dto.MajorId,
//                RefreshToken = string.Empty,
//                RefreshTokenExpiryTime = DateTime.UtcNow
//            };

//            await _userService.AddAsync(user);

//            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
//        }

//    }
//}
