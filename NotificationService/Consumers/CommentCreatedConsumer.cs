using Contracts;
using MassTransit;

namespace NotificationService.Consumers
{
    public class CommentCreatedConsumer : IConsumer<CommentCreatedContract>
    {
        public async Task Consume(ConsumeContext<CommentCreatedContract> context)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"Handling event of type: [CommentCreated]");
            Console.WriteLine($"New comment created on Post ID: {context.Message.PostId} by User: {context.Message.UserName}");
            Console.WriteLine($"Comment Content: {context.Message.Content}");
            Console.WriteLine($"Timestamp: {context.Message.Timestamp}");
            Console.WriteLine($"Sending Email to: {context.Message.OwnerEmail}");

            Console.ResetColor();
        }
    }
}
