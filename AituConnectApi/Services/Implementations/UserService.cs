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
    }
}
