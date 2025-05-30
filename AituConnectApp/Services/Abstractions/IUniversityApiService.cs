using AituConnectApp.Dto;

namespace AituConnectApp.Services.Abstractions
{
    public interface IUniversityApiService
    {
        public Task<List<UniversityDto>> GetAllAsync();
    }
}
