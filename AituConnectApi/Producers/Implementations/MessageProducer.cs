using AituConnectApi.Contracts;
using AituConnectApi.Producers.Abstractions;
using MassTransit;

namespace AituConnectApi.Producers.Implementations
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessagePayload
        => await _publishEndpoint.Publish(message, cancellationToken);
    }

}
