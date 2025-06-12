namespace AituConnectApp.Dto.Responses
{
    public class PostDetailsResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string OwnerName { get; set; }
        public List<string> Subjects { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }

    }
}
