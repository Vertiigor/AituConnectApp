using Contracts;

namespace AituConnectApi.Producers.Abstractions
{
    public interface IMessageProducer
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessagePayload;
    }
}
