using MassTransit;
using NotificationService.Consumers;

namespace NotificationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CommentCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var section = builder.Configuration.GetSection("RabbitMq");

                cfg.Host(section["Host"], h =>
                {
                    h.Username(section["Username"]);
                    h.Password(section["Password"]);
                });

                cfg.ReceiveEndpoint("create-comment-queue", e =>
                {
                    e.ConfigureConsumer<CommentCreatedConsumer>(context);
                });
            });
        });

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
