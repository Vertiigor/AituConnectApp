using AituConnectApi.Models;

namespace AituConnectApi.Services.Abstractions
{
    public interface IUserService : IService<User>
    {
        public Task<User> GetByRefreshTokenAsync(string refreshToken);
        public Task<User> GetByUsernameAsync(string username);
        public Task<bool> VerifyPasswordAsync(User user, string password);
    }
}
