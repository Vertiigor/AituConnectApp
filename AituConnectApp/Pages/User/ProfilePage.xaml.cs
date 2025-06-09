using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages.User;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfilePageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}