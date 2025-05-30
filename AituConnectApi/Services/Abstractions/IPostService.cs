using AituConnectApi.Models;

namespace AituConnectApi.Services.Abstractions
{
    public interface IPostService : IService<Post>
    {
        public Task<IEnumerable<Post>> GetAllByOwnerId(string ownerId);
        public Task<IEnumerable<Post>> GetAllByUniversity(string university);

    }
}
