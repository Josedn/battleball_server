using Fleck;

namespace BattleBall.Net
{
    public interface ISocketHandler
    {
        void HandleMessage(IWebSocketConnection socket, string message);
        void HandleDisconnect(IWebSocketConnection socket);
        void HandleStartClient(IWebSocketConnection socket);
    }
}
