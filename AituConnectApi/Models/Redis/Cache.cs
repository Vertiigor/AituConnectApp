namespace AituConnectApi.Models.Redis
{
    public class Cache<T> where T : ICachable
    {
        public string Key { get; set; }
        public T Payload { get; set; }
    }
}
