using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;

namespace AituConnectApi.Services.Implementations
{
    public class MajorService : Service<Major>, IMajorService
    {
        private readonly IMajorRepository _majorRepository;

        public MajorService(IMajorRepository repository) : base(repository)
        {
            _majorRepository = repository;
        }
    }
}
