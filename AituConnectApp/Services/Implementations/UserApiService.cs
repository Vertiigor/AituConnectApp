using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class UserApiService : ApiService, IUserApiService
    {
        public UserApiService(HttpClient client, IOptions<ApiSettings> settings) : base(client, settings)
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
    }
}
