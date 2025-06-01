using AituConnectApi.Dto;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.UserName) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Username and password are required.");

            var user = await _userService.GetByUsernameAsync(loginDto.UserName);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var isPasswordValid = await _userService.VerifyPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid credentials.");

            var tokens = await _tokenService.GenerateTokens(user);

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null || string.IsNullOrEmpty(tokenDto.RefreshToken))
            {
                return BadRequest("Invalid token data.");
            }
            // Logic to validate the refresh token and generate new tokens
            var user = await _tokenService.ValidateRefreshTokenAsync(tokenDto.RefreshToken);

            if (user == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId != user.Id)
            {
                return Unauthorized("Access token and refresh token mismatch.");
            }

            var newTokens = await _tokenService.GenerateTokens(user);

            return Ok(newTokens);
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUser([FromBody] SignUpDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var user = new Models.User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = ComputeSha256Hash(dto.Password),
                UniversityId = dto.UniversityId,
                MajorId = dto.MajorId
            };

            await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] SignUpDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            await _userService.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(id);

            return NoContent();
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
