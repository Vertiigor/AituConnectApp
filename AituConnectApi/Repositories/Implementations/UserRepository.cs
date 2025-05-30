using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;

namespace AituConnectApi.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }
    }
}
