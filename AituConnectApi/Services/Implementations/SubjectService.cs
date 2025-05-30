using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;

namespace AituConnectApi.Services.Implementations
{
    public class SubjectService : Service<Subject>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository) : base(subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
    }
}
