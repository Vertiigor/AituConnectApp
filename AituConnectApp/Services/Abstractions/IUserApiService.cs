using AituConnectApp.Dto;

namespace AituConnectApp.Services.Abstractions
{
    public interface IUserApiService
    {
        public Task<bool> CreateAsync(SignUpDto user);
    }
}
