using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;

namespace AituConnectApi.Repositories.Implementations
{
    public class UniversityRepository : Repository<University>, IUniversityRepository
    {
        public UniversityRepository(ApplicationContext context) : base(context) { }
    }
}
