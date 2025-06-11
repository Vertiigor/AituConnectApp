namespace AituConnectApp.Dto.Requests
{
    public class CreatePostRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Subjects { get; set; }
    }
}
