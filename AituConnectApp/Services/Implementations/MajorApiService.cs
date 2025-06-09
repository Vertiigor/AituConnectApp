using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class MajorApiService : ApiService, IMajorApiService
    {

        public MajorApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<List<MajorResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MajorResponseDto>>($"{_settings.MajorsEndpoints.Base}/{_settings.MajorsEndpoints.GetAll}");
        }
    }
}
