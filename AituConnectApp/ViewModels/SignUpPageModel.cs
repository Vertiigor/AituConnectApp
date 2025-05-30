using AituConnectApp.Dto;
using AituConnectApp.Pages;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Services.Implementations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace AituConnectApp.ViewModels
{
    public partial class SignUpPageModel : ObservableObject, IQueryAttributable
    {
        private readonly IUserApiService _userApiService;
        private readonly IUniversityApiService _universityApiService;
        private readonly IMajorApiService _majorApiService;

        public SignUpPageModel(IUserApiService userApiService, IUniversityApiService universityApiService, IMajorApiService majorApiService)
        {
            _userApiService = userApiService;
            _universityApiService = universityApiService;
            _majorApiService = majorApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await LoadData(); // ✅ called after navigation
        }

        private async Task LoadData()
        {
            Universities = await _universityApiService.GetAllAsync();
            Majors = await _majorApiService.GetAllAsync();
        }
        
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool showPassword;

        [ObservableProperty]
        private bool isPassword = true;

        [ObservableProperty]
        private List<UniversityDto> universities;

        [ObservableProperty]
        private List<MajorDto> majors;

        [ObservableProperty]
        private UniversityDto selectedUniversity;

        [ObservableProperty]
        private MajorDto selectedMajor;

        partial void OnShowPasswordChanged(bool value)
        {
            IsPassword = !value;
        }

        [RelayCommand]
        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }

            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 digit.",
                    "OK");
                return;
            }

            SignUpDto user = new SignUpDto()
            {
                UserName = Username,
                Password = Password,
                Email = Email,
                UniversityId = SelectedUniversity?.Id ?? "",
                MajorId = SelectedMajor?.Id ?? ""
            };

            bool success = await _userApiService.CreateAsync(user);
            if (success)
            {
#if DEBUG
                await Shell.Current.DisplayAlert("Success", "User has been created!", "ОК");
#endif
                string token = Guid.NewGuid().ToString();
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }
    }
}
