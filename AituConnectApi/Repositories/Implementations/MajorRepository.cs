using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;

namespace AituConnectApi.Repositories.Implementations
{
    public class MajorRepository : Repository<Major>, IMajorRepository
    {
        public MajorRepository(ApplicationContext context) : base(context) { }
    }
}
