using AituConnectApp.Dto.Requests;
using AituConnectApp.Dto.Responses;

namespace AituConnectApp.Services.Abstractions
{
    public interface IPostApiService
    {
        public Task<bool> CreateAsync(CreatePostRequestDto dto);
        public Task<List<PostDetailsResponseDto>> GetAllByUniversityAsync();
    }
}
