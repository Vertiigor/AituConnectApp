using AituConnectApp.Dto.Requests;
using AituConnectApp.Dto.Responses;
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

        public async Task<List<PostDetailsResponseDto>> GetAllByUniversityAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDetailsResponseDto>>($"{_settings.PostsEndpoints.Base}/{_settings.PostsEndpoints.GetAllByUniversity}");
        }

        public async Task<PostDetailsResponseDto> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<PostDetailsResponseDto>($"{_settings.PostsEndpoints.Base}/{_settings.PostsEndpoints.GetById}/{id}");
        }
    }
}
