﻿using AituConnectApi.Dto.Requests;
using AituConnectApi.Dto.Responses;
using AituConnectApi.Extensions;
using AituConnectApi.Models.Redis;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICacheService _cacheService;

        public UserController(IUserService userService, ITokenService tokenService, ICacheService cacheService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _cacheService = cacheService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
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

            await SetCache(user);

            return Ok(tokens);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenDto)
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

            var userId = this.GetUserIdFromExpiredToken(_tokenService, tokenDto.AccessToken);

            if (userId != user.Id)
            {
                return Unauthorized("Access token and refresh token mismatch.");
            }

            var newTokens = await _tokenService.GenerateTokens(user);

            await SetCache(user);

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

        [HttpGet("profile-info")]
        [Authorize]
        public async Task<IActionResult> GetProfileInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var userId = this.GetUserId();

            Console.WriteLine($"ID: {userId}");

            Console.ResetColor();

            if (userId == null) return Unauthorized();

            var cache = await _cacheService.GetAsync<UserCache>(userId);

            var dto = new ProfileResponseDto
            {
                UserName = cache.Username,
                Email = cache.Email,
            };

            return Ok(dto);
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
        public async Task<IActionResult> CreateUser([FromBody] SignUpRequestDto dto)
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
                MajorId = dto.MajorId,
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = DateTime.UtcNow
            };

            await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] SignUpRequestDto dto)
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

        private async Task SetCache(Models.User user)
        {
            var cache = new Cache<UserCache>
            {
                Key = user.Id,
                Payload = new UserCache
                {
                    Username = user.UserName,
                    Email = user.Email,
                    UniversityId = user.UniversityId,
                    MajorId = user.MajorId
                }
            };

            await _cacheService.SetAsync(cache);
        }
    }
}
