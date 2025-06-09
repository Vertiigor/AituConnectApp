using AituConnectApp.Pages.Post;
using AituConnectApp.Pages.User;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AituConnectApp.ViewModels
{
    public partial class MainPageModel : ObservableObject
    {

        //[RelayCommand]
        //private async Task Login()
        //{
        //    await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        //}

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public async Task CheckLoginStatusAsync()
        {
            var token = await SecureStorage.GetAsync("access_token");
            IsLoggedIn = !string.IsNullOrEmpty(token);
        }


        public async Task LoadUsernameAsync()
        {
            var username = await SecureStorage.GetAsync("username");
            Username = username ?? "Guest";
        }

        [RelayCommand]
        private async Task SignUp()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
        }

        [RelayCommand]
        private async Task LogIn()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        [RelayCommand]
        private async Task CreatePost()
        {
            await Shell.Current.GoToAsync($"{nameof(CreatePostPage)}");
        }

        [RelayCommand]
        private async Task Profile()
        {
            var accessToken = await SecureStorage.GetAsync("access_token");
            var refreshToken = await SecureStorage.GetAsync("refresh_token");

            await Shell.Current.DisplayAlert("Error", $"{accessToken}\n{refreshToken}", "OK");

            await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
        }

        [RelayCommand]
        private async Task LogOut()
        {
            SecureStorage.Remove("access_token");
            SecureStorage.Remove("refresh_token");
            SecureStorage.Remove("username");
            IsLoggedIn = false;
            Username = "Guest";
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
