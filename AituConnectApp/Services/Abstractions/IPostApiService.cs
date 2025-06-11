using AituConnectApp.Dto.Requests;

namespace AituConnectApp.Services.Abstractions
{
    public interface IPostApiService
    {
        public Task<bool> CreateAsync(CreatePostRequestDto dto);
    }
}
