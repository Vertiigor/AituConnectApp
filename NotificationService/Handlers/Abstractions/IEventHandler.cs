using NotificationService.Contracts;

namespace NotificationService.Handlers.Abstractions
{
    public interface IEventHandler
    {
        Task HandleAsync(MessageEnvelope envelope);
    }
}
