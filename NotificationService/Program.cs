using MassTransit;
using NotificationService.Connections.RabbitMq;
using NotificationService.Consumers;
using NotificationService.Settings.RabbitMq;
using RabbitMQ.Client;

namespace NotificationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CreatedCommentConsumer>();

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
                    // don't create message-type exchanges
                    e.ConfigureConsumeTopology = false;

                    // bind to your publisher’s exchange
                    e.Bind("create-comment", x =>
                    {
                        x.ExchangeType = ExchangeType.Fanout;
                    });

                    e.ConfigureConsumer<CreatedCommentConsumer>(context);
                });
            });
        });



        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

        //builder.Services.AddScoped<EventHandler, CreatedCommentEventHandler>();

        ///builder.Services.AddScoped<HandlerRouter>();

        //builder.Services.AddHostedService<CommentsQueueConsumer>();

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
