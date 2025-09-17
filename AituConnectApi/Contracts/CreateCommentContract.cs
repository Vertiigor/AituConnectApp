namespace AituConnectApi.Contracts
{
    public class CreateCommentContract : IMessagePayload
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string Content { get; set; }
        public string OwnerEmail { get; set; } = string.Empty;
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
