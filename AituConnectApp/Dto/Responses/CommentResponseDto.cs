namespace AituConnectApp.Dto.Responses
{
    public class CommentResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Replies (nested structure)
        public List<CommentResponseDto> Replies { get; set; } = new();
    }
}
