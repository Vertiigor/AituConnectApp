using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class UniversityApiService : ApiService, IUniversityApiService
    {
        public UniversityApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<List<UniversityResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UniversityResponseDto>>($"{_settings.UniversitiesEndpoints.Base}/{_settings.UniversitiesEndpoints.GetAll}");
        }
    }
}
