using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;

namespace AituConnectApi.Repositories.Implementations
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
