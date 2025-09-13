namespace AituConnectApi.Models
{
    public class Comment
    {
        public string Id { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string? CommentId { get; set; } = string.Empty; // For nested comments (replies)  
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
        public Comment? ParentComment { get; set; }
    }
}
