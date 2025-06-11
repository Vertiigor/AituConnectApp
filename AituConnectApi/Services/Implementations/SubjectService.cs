using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Services.Implementations
{
    public class SubjectService : Service<Subject>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository) : base(subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<Subject>> GetSubjectsByIds(List<string> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<Subject>();
            }

            var subjects = await _subjectRepository.GetAllAsQueryable()
                .Where(subject => ids.Contains(subject.Id))
                .ToListAsync();

            return subjects ?? new List<Subject>();
        }
    }
}
