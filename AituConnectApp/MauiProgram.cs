using AituConnectApp.Pages;
using AituConnectApp.Pages.Post;
using AituConnectApp.Pages.User;
using AituConnectApp.Services;
using AituConnectApp.Services.Abstractions;
using AituConnectApp.Services.Implementations;
using AituConnectApp.Settings.Api.AituConnect;
using AituConnectApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AituConnectApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("AituConnectApp.appsettings.json");
            if (stream == null)
                throw new FileNotFoundException("Could not find embedded resource: appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

            var settings = config.GetRequiredSection("AituConnectApi").Get<ApiSettings>();

            builder.Services.Configure<ApiSettings>(config.GetRequiredSection("AituConnectApi"));

            builder.Services.AddTransient<AuthentificatedHttpClientHandler>();

            builder.Services.AddHttpClient("ApiClient")
                .AddHttpMessageHandler<AuthentificatedHttpClientHandler>();

            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IUniversityApiService, UniversityApiService>();
            builder.Services.AddScoped<IMajorApiService, MajorApiService>();
            builder.Services.AddScoped<ISubjectApiService, SubjectApiService>();
            builder.Services.AddScoped<IPostApiService, PostApiService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageModel>();

            builder.Services.AddSingleton<SignUpPage>();
            builder.Services.AddSingleton<SignUpPageModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LogInPageModel>();

            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<ProfilePageModel>();

            builder.Services.AddSingleton<CreatePostPage>();
            builder.Services.AddSingleton<CreatePostPageModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
