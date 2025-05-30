using AituConnectApp.Pages;
using AituConnectApp.Pages.User;
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
            //builder.Services.Configure<UsersEndpointsSettings>(config.GetRequiredSection("AituConnectApi:UsersEndpoints"));
            //builder.Services.Configure<UniversitiesEndpointsSettings>(config.GetRequiredSection("AituConnectApi:UniversitiesEndpoints"));
            //builder.Services.Configure<MajorsEndpointsSettings>(config.GetRequiredSection("AituConnectApi:MajorsEndpoints"));


            builder.Services.AddHttpClient<ApiService>();
            //builder.Services.AddHttpClient<ApiService>(client =>
            //{
            //    client.BaseAddress = new Uri(settings.BaseUrl);
            //});

            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IUniversityApiService, UniversityApiService>();
            builder.Services.AddScoped<IMajorApiService, MajorApiService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageModel>();

            builder.Services.AddSingleton<SignUpPage>();
            builder.Services.AddSingleton<SignUpPageModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
