using AituConnectApi.Models;

namespace AituConnectApi.Repositories.Abstractions
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IEnumerable<Post>> GetAllByUniversity(string universityId);
        public IQueryable<Post> GetAllWithIncludes();
    }
}
