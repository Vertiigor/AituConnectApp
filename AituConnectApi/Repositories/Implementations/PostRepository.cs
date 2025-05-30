using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context) { }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await GetAllWithIncludes()
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllByUniversity(string universityId)
        {
            return await GetAllWithIncludes()
                .Where(p => p.UniversityId == universityId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllByOwnerId(string userId)
        {
            return await GetAllWithIncludes()
                .Where(p => p.OwnerId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public IQueryable<Post> GetAllWithIncludes()
        {
            return GetAllAsQueryable()
                .Include(p => p.User)
                .Include(p => p.Subjects);
        }
    }
}
