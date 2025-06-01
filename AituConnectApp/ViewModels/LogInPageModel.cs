using AituConnectApp.Pages;
using AituConnectApp.Pages.User;
using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AituConnectApp.ViewModels
{
    public partial class LogInPageModel : ObservableObject
    {
        private readonly IUserApiService _userApiService;

        public LogInPageModel(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isPassword = true;

        [RelayCommand]
        private async Task Signin()
        {
            await Shell.Current.GoToAsync($"///{nameof(SignUpPage)}");
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }

            var success = await _userApiService.LignInAsync(new Dto.LoginDto { UserName = Username, Password = Password });

            if (success)
            {
                SecureStorage.SetAsync("username", Username);
                try
                {
#if DEBUG
                    await Shell.Current.DisplayAlert("Success", "User logged in", "OK");
#endif
                    await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Login] Ошибка: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "ОК");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Erroe", "Something went wrong...", "OK");
            }
        }
    }
}
