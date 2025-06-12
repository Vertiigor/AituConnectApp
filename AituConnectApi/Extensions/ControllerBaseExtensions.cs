using AituConnectApi.Services.Abstractions;
using AituConnectApi.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AituConnectApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetUserId(this ControllerBase controller)
        {
            return controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserIdFromExpiredToken(this ControllerBase controller, ITokenService tokenService, string accessToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);

            return principal.Claims.First().Value;
        }
    }
}
