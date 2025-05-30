namespace AituConnectApp.Settings.Api.AituConnect
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public UsersEndpointsSettings UsersEndpoints { get; set; }
        public UniversitiesEndpointsSettings UniversitiesEndpoints { get; set; }
        public MajorsEndpointsSettings MajorsEndpoints { get; set; }
    }
}
