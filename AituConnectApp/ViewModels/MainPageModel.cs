﻿using AituConnectApp.Dto.Responses;
using AituConnectApp.Pages.Post;
using AituConnectApp.Pages.User;
using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AituConnectApp.ViewModels
{
    public partial class MainPageModel : ObservableObject, IQueryAttributable
    {
        private string _username;

        private readonly IPostApiService _postApiService;

        [ObservableProperty]
        private ObservableCollection<PostDetailsResponseDto> posts;

        public MainPageModel(IPostApiService postApiService)
        {
            _postApiService = postApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await LoadData(); // ✅ called after navigation
        }

        public async Task LoadData()
        {
            var dto = await _postApiService.GetAllByUniversityAsync();
            Posts = new ObservableCollection<PostDetailsResponseDto>(dto);
        }

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
        private async Task OpenPostDetails(string postId)
        {
            await Shell.Current.GoToAsync($"///postdetails?id={postId}");
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
