using AituConnectApp.Settings.Api.AituConnect;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace AituConnectApp.Services.Abstractions
{
    public abstract class ApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly ApiSettings _settings;

        public ApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _settings = settings.Value;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }
    }
}
