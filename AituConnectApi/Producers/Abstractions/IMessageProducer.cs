using AituConnectApi.Contracts;

namespace AituConnectApi.Producers.Abstractions
{
    public interface IMessageProducer
    {
        public Task PublishMessageAsync<T>(
            string eventType,
            T payload,
            string exchange = "",
            string routingKey = "",
            bool durable = true,
            Dictionary<string, object?> arguments = null)
            where T : IMessagePayload;
    }
}
