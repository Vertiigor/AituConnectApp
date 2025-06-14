﻿using AituConnectApp.Pages;
using AituConnectApp.Pages.Post;
using AituConnectApp.Pages.User;

namespace AituConnectApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(CreatePostPage), typeof(CreatePostPage));
            Routing.RegisterRoute(nameof(PostDetailsPage), typeof(PostDetailsPage));
        }
    }
}
