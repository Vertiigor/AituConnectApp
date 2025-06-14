namespace AituConnectApp.Dto.Requests
{
    public class CreateCommentRequestDto
    {
        public string PostId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
