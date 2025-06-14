using AituConnectApp.Dto.Requests;
using AituConnectApp.Dto.Responses;
using AituConnectApp.Pages;
using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AituConnectApp.ViewModels
{
    public partial class PostDetailsPageModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private PostDetailsResponseDto post;

        [ObservableProperty]
        private ObservableCollection<CommentResponseDto> comments;

        [ObservableProperty]
        public string newCommentText;

        private readonly IPostApiService _postApiService;
        private readonly ICommentApiService _commentApiService;

        public PostDetailsPageModel(IPostApiService postApiService, ICommentApiService commentApiService)
        {
            _postApiService = postApiService;
            _commentApiService = commentApiService;
        }

        [RelayCommand]
        private async Task SubmitComment()
        {
            if (newCommentText == null || string.IsNullOrEmpty(newCommentText))
            {
                await Shell.Current.DisplayAlert("Error", "The comment must have a body. Text anything.", "OK");
                return;
            }

            var dto = new CreateCommentRequestDto
            {
                PostId = post.Id,
                Content = newCommentText
            };

            bool success = await _commentApiService.CreateAsync(dto);

            if (success)
            {
#if DEBUG
                await Shell.Current.DisplayAlert("Success", "Comment has been created!", "ОК");
#endif
                await Shell.Current.GoToAsync($"///postdetails?id={post.Id}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var id))
            {
                var postId = id as string;
                Post = await _postApiService.GetByIdAsync(postId);
                if (Post != null)
                {
                    Comments = new ObservableCollection<CommentResponseDto>(Post.Comments);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Post not found", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                }
            }
        }
    }
}
