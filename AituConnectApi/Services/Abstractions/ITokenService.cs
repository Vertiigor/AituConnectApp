using AituConnectApi.Dto;
using AituConnectApi.Models;
using System.Security.Claims;

namespace AituConnectApi.Services.Abstractions
{
    public interface ITokenService
    {
        public Task<TokenDto> GenerateTokens(User user);
        public Task<User> ValidateRefreshTokenAsync(string refreshToken);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
