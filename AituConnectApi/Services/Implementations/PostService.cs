using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Services.Implementations
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository repository) : base(repository)
        {
            _postRepository = repository;
        }

        public async Task<IEnumerable<Post>> GetAllByUniversity(string university)
        {
            return await _postRepository.GetAllAsQueryable()
                .Where(p => p.UniversityId == university)
                .Include(p => p.User)
                .Include(p => p.Subjects)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
