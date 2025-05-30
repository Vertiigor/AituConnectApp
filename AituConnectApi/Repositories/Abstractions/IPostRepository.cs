using AituConnectApi.Models;

namespace AituConnectApi.Repositories.Abstractions
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IEnumerable<Post>> GetAllByUniversity(string universityId);
        public Task<IEnumerable<Post>> GetAllByOwnerId(string ownerId);
        public IQueryable<Post> GetAllWithIncludes();
    }
}
