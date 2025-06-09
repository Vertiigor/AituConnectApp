using AituConnectApp.Dto.Responses;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class SubjectApiService : ApiService, ISubjectApiService
    {
        public SubjectApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings) : base(httpClientFactory, settings)
        {
        }

        public async Task<List<SubjectResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SubjectResponseDto>>($"{_settings.SubjectsEndpoints.Base}/{_settings.SubjectsEndpoints.GetAll}");
        }
    }
}
