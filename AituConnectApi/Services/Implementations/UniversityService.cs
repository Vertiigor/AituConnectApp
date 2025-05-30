using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;

namespace AituConnectApi.Services.Implementations
{
    public class UniversityService : Service<University>, IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository repository) : base(repository)
        {
            _universityRepository = repository;
        }
    }
}
