using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;

namespace AituConnectApp.Services.Abstractions
{
    public abstract class ApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly ApiSettings _settings;

        public ApiService(HttpClient client, IOptions<ApiSettings> settings)
        {
            _httpClient = client;
            _settings = settings.Value;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }
    }
}
