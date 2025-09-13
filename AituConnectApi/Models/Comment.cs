namespace AituConnectApi.Models
{
    public class Comment
    {
        public string Id { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        // Parent comment (nullable for top-level comments)
        public string? ParentId { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
        public Comment? ParentComment { get; set; }
        public List<Comment> Replies { get; set; } = new();
    }

}
