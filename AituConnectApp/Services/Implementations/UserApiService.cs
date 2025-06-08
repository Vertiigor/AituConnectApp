using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class UserApiService : ApiService, IUserApiService
    {
        public UserApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<bool> CreateAsync(SignUpDto user)
        {
            var response = await _httpClient.PostAsJsonAsync<SignUpDto>($"{_settings.UsersEndpoints.Base}/{_settings.UsersEndpoints.Add}", user);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<ProfileResponseDto> GetProfileInfo()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, $"{_settings.UsersEndpoints.Base}/{_settings.UsersEndpoints.ProfileInfo}");
            //var response = await _httpClient.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    return await response.Content.ReadFromJsonAsync<ProfileResponseDto>();
            //}
            //else
            //{
            //    throw new Exception($"Failed to get profile.");
            //}

            return await _httpClient.GetFromJsonAsync<ProfileResponseDto>($"{_settings.UsersEndpoints.Base}/{_settings.UsersEndpoints.ProfileInfo}");
        }

        public async Task<bool> LogInAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_settings.UsersEndpoints.Base}/{_settings.UsersEndpoints.Login}", new LoginDto
            {
                UserName = dto.UserName,
                Password = dto.Password
            });

            if (response.IsSuccessStatusCode)
            {
                var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();

                await SecureStorage.SetAsync("access_token", tokenDto.AccessToken);
                await SecureStorage.SetAsync("refresh_token", tokenDto.RefreshToken);

                return true;
                // Proceed to the app
            }
            else
            {
                // Show error
                return false;
            }

        }
    }
}
