using System.Text.Json;

namespace NotificationService.Contracts
{
    public class MessageEnvelope
    {
        public string EventType { get; set; }
        public object Payload { get; set; }

        public T GetPayload<T>()
        {
            if (Payload is JsonElement jsonElement)
            {
                return jsonElement.Deserialize<T>();
            }
            throw new InvalidOperationException("Payload is not a JsonElement");
        }
    }
}
