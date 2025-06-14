using AituConnectApp.Dto.Requests;

namespace AituConnectApp.Services.Abstractions
{
    public interface ICommentApiService
    {
        public Task<bool> CreateAsync(CreateCommentRequestDto dto);
    }
}
