using MassTransit;

namespace Contracts
{
    [ExcludeFromTopology]
    public interface IMessagePayload
    {
        DateTime Timestamp { get; }
    }
}
