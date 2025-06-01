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
            }
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
