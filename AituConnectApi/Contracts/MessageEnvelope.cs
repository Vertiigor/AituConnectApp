namespace AituConnectApi.Contracts
{
    public class MessageEnvelope<T> where T : IMessagePayload
    {
        public string EventType { get; set; }
        public T Payload { get; set; }
    }
}
