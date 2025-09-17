using NotificationService.Contracts;
using EventHandler = NotificationService.Handlers.Abstractions.EventHandler;

namespace NotificationService.Handlers.Implementations.Comments
{
    public class CreatedCommentEventHandler : EventHandler
    {
        public override string EventType => "CreatedComment";

        public override async Task HandleAsync(MessageEnvelope envelope)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"[CreatedCommentEventHandler] Handling event of type: {envelope.EventType}");

            Console.ResetColor();
        }
    }
}
