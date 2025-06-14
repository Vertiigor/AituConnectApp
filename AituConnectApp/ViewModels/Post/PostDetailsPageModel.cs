using AituConnectApp.Dto.Responses;
using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AituConnectApp.ViewModels
{
    public partial class PostDetailsPageModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private PostDetailsResponseDto post;

        private readonly IPostApiService _postApiService;

        public PostDetailsPageModel(IPostApiService postApiService)
        {
            _postApiService = postApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var id))
            {
                var postId = id as string;
                Post = await _postApiService.GetByIdAsync(postId);
            }
        }
    }
}
