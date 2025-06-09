namespace AituConnectApi.Dto.Requests
{
    public class CreatePostRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        List<string> SubjectId { get; set; }
    }
}
