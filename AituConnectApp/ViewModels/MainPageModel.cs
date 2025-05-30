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

        [RelayCommand]
        private async Task SignUp()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
        }
    }
}
