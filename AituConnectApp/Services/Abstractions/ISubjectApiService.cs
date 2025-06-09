using AituConnectApp.Dto.Responses;

namespace AituConnectApp.Services.Abstractions
{
    public interface ISubjectApiService
    {
        public Task<List<SubjectResponseDto>> GetAllAsync();
    }
}
