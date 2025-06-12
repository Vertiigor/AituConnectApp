using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MainPageModel vm)
            {
                await vm.CheckLoginStatusAsync();
                await vm.LoadUsernameAsync();
                await vm.LoadData(); // Load posts when the page appears
            }
        }
    }
}
