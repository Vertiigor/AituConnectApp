using NotificationService.Contracts;

namespace NotificationService.Handlers.Abstractions
{
    public abstract class EventHandler
    {
        public abstract string EventType { get; }

        public abstract Task HandleAsync(MessageEnvelope envelope);
    }
}
