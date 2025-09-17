using NotificationService.Connections.RabbitMq;
using NotificationService.Contracts;
using NotificationService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.Consumers
{
    public class CommentsQueueConsumer : BackgroundService
    {
        protected readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommentsQueueConsumer(IRabbitMqConnection rabbitMqConnection, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var c = await _rabbitMqConnection.GetConnectionAsync();
                Console.WriteLine("✅ Connected to RabbitMQ successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to connect: {ex.Message}");
            }


            var connection = await _rabbitMqConnection.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "comments",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            var handlerRouter = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<HandlerRouter>();

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var envelope = JsonSerializer.Deserialize<MessageEnvelope>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (envelope != null)
                    {
                        Console.WriteLine($"✅ Received message: {envelope.EventType}");
                        handlerRouter.TryGetValue(envelope.EventType, out var handler);
                        await handler.HandleAsync(envelope);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error processing message: {ex.Message}");
                }

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue: "comments",
                autoAck: false,
                consumer: consumer);
        }
    }
}
