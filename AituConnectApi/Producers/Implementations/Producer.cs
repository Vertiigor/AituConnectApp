using AituConnectApi.Connections.RabbitMq;
using AituConnectApi.Contracts;
using AituConnectApi.Producers.Abstractions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AituConnectApi.Producers.Implementations
{
    public class Producer : IMessageProducer
    {
        private readonly IRabbitMqConnection _connection;

        public Producer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishMessageAsync<T>(
            string eventType,
            T payload,
            string exchange = "",
            string routingKey = "",
            bool durable = true,
            Dictionary<string, object?> arguments = null)
            where T : IMessagePayload
        {
            var connection = await _connection.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // Declare the topic exchange if not already declared
            await channel.ExchangeDeclareAsync(exchange, ExchangeType.Topic, durable: true);

            // Prepare the message
            var envelope = new MessageEnvelope<T>
            {
                EventType = eventType,
                Payload = payload
            };

            var json = JsonSerializer.Serialize(envelope);
            var body = Encoding.UTF8.GetBytes(json);

            // Publish the message using routing key
            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey, // e.g., "user.registration"
                body: body);

        }
    }
}
