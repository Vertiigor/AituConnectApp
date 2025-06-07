using AituConnectApi.Dto.Requests;
using AituConnectApi.Models;
using System.Security.Claims;

namespace AituConnectApi.Services.Abstractions
{
    public interface ITokenService
    {
        public Task<TokenRequestDto> GenerateTokens(User user);
        public Task<User> ValidateRefreshTokenAsync(string refreshToken);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
