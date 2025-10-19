using MassTransit;

namespace NotificationService.Contracts
{
    [ExcludeFromTopology]
    public interface IMessagePayload
    {
        DateTime Timestamp { get; }
    }
}
