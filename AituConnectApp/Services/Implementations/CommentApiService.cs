using AituConnectApp.Dto.Requests;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class CommentApiService : ApiService, ICommentApiService
    {
        public CommentApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<bool> CreateAsync(CreateCommentRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCommentRequestDto>($"{_settings.CommentsEndpoints.Base}/{_settings.CommentsEndpoints.Add}", dto);

            if (!response.IsSuccessStatusCode)
            {
                return false;

            }

            return true;
        }
    }
}
