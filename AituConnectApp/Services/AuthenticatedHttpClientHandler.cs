using AituConnectApp.Dto;
using AituConnectApp.Settings.Api.AituConnect;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AituConnectApp.Services
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        protected readonly ApiSettings _settings;

        public AuthenticatedHttpClientHandler(ApiSettings settings)
        {
            _settings = settings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await SecureStorage.GetAsync("access_token");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            // Try refresh on 401 Unauthorized
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await TryRefreshTokenAsync();
                if (refreshed)
                {
                    token = await SecureStorage.GetAsync("access_token");

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }

        private async Task<bool> TryRefreshTokenAsync()
        {
            var accessToken = await SecureStorage.GetAsync("access_token");
            var refreshToken = await SecureStorage.GetAsync("refresh_token");

            var refreshDto = new
            {
                accessToken,
                refreshToken
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync($"{_settings.BaseUrl}/{_settings.UsersEndpoints.RefreshToken}", refreshDto);

            if (!response.IsSuccessStatusCode) return false;

            var tokenResult = await response.Content.ReadFromJsonAsync<TokenDto>();

            await SecureStorage.SetAsync("access_token", tokenResult.AccessToken);
            await SecureStorage.SetAsync("refresh_token", tokenResult.RefreshToken);

            return true;
        }
    }

}
