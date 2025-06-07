using AituConnectApp.Dto;

namespace AituConnectApp.Services.Abstractions
{
    public interface IUserApiService
    {
        public Task<bool> CreateAsync(SignUpDto user);
        public Task<bool> LogInAsync(LoginDto dto);
        public Task<ProfileResponseDto> GetProfileInfo();
    }
}
