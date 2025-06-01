using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages.User;

public partial class LoginPage : ContentPage
{
	public LoginPage(LogInPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}