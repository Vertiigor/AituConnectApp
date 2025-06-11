using AituConnectApp.Dto.Requests;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class PostApiService : ApiService, IPostApiService
    {
        public PostApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<bool> CreateAsync(CreatePostRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreatePostRequestDto>($"{_settings.PostsEndpoints.Base}/{_settings.PostsEndpoints.Add}", dto);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
