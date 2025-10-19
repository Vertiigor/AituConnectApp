using MassTransit;

namespace AituConnectApi.Contracts
{
    [ExcludeFromTopology]
    public interface IMessagePayload
    {
        DateTime Timestamp { get; }
    }
}
