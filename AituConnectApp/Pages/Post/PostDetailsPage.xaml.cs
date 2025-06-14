using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages.Post;

public partial class PostDetailsPage : ContentPage
{
    public PostDetailsPage(PostDetailsPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}