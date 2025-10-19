using MassTransit;
using NotificationService.Contracts;

namespace NotificationService.Consumers
{
    public class CreatedCommentConsumer : IConsumer<CreateCommentContract>
    {
        public async Task Consume(ConsumeContext<CreateCommentContract> context)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"[CreatedCommentEventHandler] Handling event of type: CreatedCommentConsumer");

            Console.ResetColor();
        }
    }
}
