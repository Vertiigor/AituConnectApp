using AituConnectApp.Dto.Requests;
using AituConnectApp.Dto.Responses;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

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
            var response = await _httpClient.GetAsync(
                $"{_settings.PostsEndpoints.Base}/{_settings.PostsEndpoints.GetAllByUniversity}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PostDetailsResponseDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<PostDetailsResponseDto>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // No posts for this university → return empty list instead of crashing
                return new List<PostDetailsResponseDto>();
            }
            else
            {
                throw new Exception($"Error fetching posts: {response.StatusCode}");
            }
        }


        public async Task<PostDetailsResponseDto> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<PostDetailsResponseDto>($"{_settings.PostsEndpoints.Base}/{_settings.PostsEndpoints.GetById}/{id}");
        }
    }
}
