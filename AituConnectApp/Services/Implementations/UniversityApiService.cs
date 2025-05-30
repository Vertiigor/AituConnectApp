using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class UniversityApiService : ApiService, IUniversityApiService
    {
        public UniversityApiService(HttpClient client, IOptions<ApiSettings> settings) : base(client, settings)
        {
        }

        public async Task<List<UniversityDto>> GetAllAsync()
        {
            //throw new Exception($"{_httpClient.BaseAddress}/{_settings.Base}/{_settings.GetAll}");
            return await _httpClient.GetFromJsonAsync<List<UniversityDto>>($"{_settings.UniversitiesEndpoints.Base}/{_settings.UniversitiesEndpoints.GetAll}");
        }
    }
}
