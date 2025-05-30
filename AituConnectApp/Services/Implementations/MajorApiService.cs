using AituConnectApp.Dto;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AituConnectApp.Services.Implementations
{
    public class MajorApiService : ApiService, IMajorApiService
    {

        public MajorApiService(HttpClient client, IOptions<ApiSettings> settings) : base(client, settings)
        {
        }

        public async Task<List<MajorDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MajorDto>>($"{_settings.MajorsEndpoints.Base}/{_settings.MajorsEndpoints.GetAll}");
        }
    }
}
