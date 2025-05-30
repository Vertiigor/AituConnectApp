using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages.User;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(SignUpPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}