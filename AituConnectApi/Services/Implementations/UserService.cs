using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;

namespace AituConnectApi.Services.Implementations
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetByRefreshTokenAsync(refreshToken);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            return await _userRepository.VerifyPasswordAsync(user, password);
        }
    }
}
