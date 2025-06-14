namespace AituConnectApi.Dto.Responses
{
    public class CommentResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
