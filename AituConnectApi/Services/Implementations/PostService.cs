using AituConnectApi.Models;
using AituConnectApi.Repositories.Abstractions;
using AituConnectApi.Services.Abstractions;

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
            return await _postRepository.GetAllByUniversity(university);
        }

        public async Task<IEnumerable<Post>> GetAllByOwnerId(string userId)
        {
            var userPosts = await _postRepository.GetAllByOwnerId(userId);

            return userPosts;
        }
    }
}
