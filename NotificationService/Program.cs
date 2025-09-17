using NotificationService.Connections.RabbitMq;
using NotificationService.Consumers;
using NotificationService.Handlers.Implementations.Comments;
using NotificationService.Services;
using NotificationService.Settings.RabbitMq;
using EventHandler = NotificationService.Handlers.Abstractions.EventHandler;

namespace NotificationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

        builder.Services.AddScoped<EventHandler, CreatedCommentEventHandler>();

        builder.Services.AddScoped<HandlerRouter>();

        builder.Services.AddHostedService<CommentsQueueConsumer>();

        // Add services to the container.
        builder.Services.AddAuthorization();


        var app = builder.Build();


        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}
