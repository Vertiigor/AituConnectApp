namespace AituConnectApi.Dto.Requests
{
    public class ReplyCommentRequestDto
    {
        public string PostId { get; set; } = string.Empty;
        public string ParentCommentId { get; set; } = string.Empty; // ID of the comment being replied to
        public string Content { get; set; } = string.Empty;
    }
}
