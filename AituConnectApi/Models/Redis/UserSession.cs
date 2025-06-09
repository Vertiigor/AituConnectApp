namespace AituConnectApi.Models.Redis
{
    public class UserSession
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string UniversityId { get; set; }
        public string MajorId { get; set; }
        public Dictionary<string, string> Data { get; set; } = new();
    }
}
