using AituConnectApi.Data;
using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Repositories.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<Comment> GetByIdAsync(string id)
        {
            return await GetAllWithIncludes()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Comment> GetAllWithIncludes()
        {
            return GetAllAsQueryable()
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(p => p.User)
                .Include(c => c.Post)
                .ThenInclude(p => p.Subjects)
                .Include(c => c.Post)
                .ThenInclude(p => p.Comments);
        }
    }
}
