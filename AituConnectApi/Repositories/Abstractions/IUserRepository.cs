using AituConnectApi.Models;

namespace AituConnectApi.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByRefreshTokenAsync(string refreshToken);
        public Task<User> GetByUsernameAsync(string username);
        public Task<bool> VerifyPasswordAsync(User user, string password);
    }
}
