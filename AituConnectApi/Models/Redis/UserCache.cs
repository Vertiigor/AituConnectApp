namespace AituConnectApi.Models.Redis
{
    public class UserCache : ICachable
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string UniversityId { get; set; }
        public string MajorId { get; set; }
    }
}
