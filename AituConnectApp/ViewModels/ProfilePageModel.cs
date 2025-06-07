using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AituConnectApp.ViewModels
{
    public partial class ProfilePageModel : ObservableObject, IQueryAttributable
    {
        private readonly IUserApiService _userApiService;

        public ProfilePageModel(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var response = await _userApiService.GetProfileInfo();

            Username = response.UserName;
            Email = response.Email;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;
    }
}
