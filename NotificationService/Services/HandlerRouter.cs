using EventHandler = NotificationService.Handlers.Abstractions.EventHandler;

namespace NotificationService.Services
{
    public class HandlerRouter
    {
        private readonly Dictionary<string, EventHandler> _handlers;

        public HandlerRouter(IEnumerable<EventHandler> handlers)
        {
            _handlers = new Dictionary<string, EventHandler>();

            // Register all handlers in the dictionary
            foreach (var handler in handlers)
            {
                _handlers[handler.EventType] = handler;
            }
        }

        public bool TryGetValue(string key, out EventHandler handler)
        {
            return _handlers.TryGetValue(key, out handler);
        }
    }
}
