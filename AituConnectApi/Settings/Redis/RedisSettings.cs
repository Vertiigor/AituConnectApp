namespace AituConnectApi.Settings.Redis
{
    public class RedisSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 6379;
        public string? Password { get; set; }
        public int ConnectRetry { get; set; } = 3;
        public int ConnectTimeout { get; set; } = 5000;
    }
}
