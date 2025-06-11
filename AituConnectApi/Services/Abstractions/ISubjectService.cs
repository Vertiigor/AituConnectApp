using AituConnectApi.Models;

namespace AituConnectApi.Services.Abstractions
{
    public interface ISubjectService : IService<Subject>
    {
        public Task<List<Subject>> GetSubjectsByIds(List<string> ids);
    }
}
