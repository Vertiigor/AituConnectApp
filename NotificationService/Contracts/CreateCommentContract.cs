namespace NotificationService.Contracts
{
    public class CreateCommentContract : IMessagePayload
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public string CommentId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string PostId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string OwnerEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
    }
}
