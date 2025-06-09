using AituConnectApp.Dto;

namespace AituConnectApp.Services.Abstractions
{
    public interface IUserApiService
    {
        public Task<bool> CreateAsync(SignUpRequestDto user);
        public Task<bool> LogInAsync(LoginRequestDto dto);
        public Task<ProfileResponseDto> GetProfileInfo();
    }
}
