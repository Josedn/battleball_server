using Fleck;

namespace BattleBall.Net
{
    internal class ConnectionManager
    { 
        private WebSocketServer server;
        private ISocketHandler messageHandler;

        internal ConnectionManager(ISocketHandler MessageHandler, int Port = 8181)
        {
            FleckLog.Level = LogLevel.Warn;
            this.server = new WebSocketServer("ws://0.0.0.0:" + Port);
            this.messageHandler = MessageHandler;
            
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    OnOpenConnection(socket);
                };
                socket.OnClose = () =>
                {
                    OnCloseConnection(socket);
                };
                socket.OnMessage = message =>
                {
                    OnMessage(socket, message);
                };
            });
        }

        internal void OnOpenConnection(IWebSocketConnection socket)
        {
            //Console.WriteLine("Open!");
            messageHandler.HandleStartClient(socket);
        }

        internal void OnCloseConnection(IWebSocketConnection socket)
        {
            //Console.WriteLine("Close!");
            messageHandler.HandleDisconnect(socket);
        }

        internal void OnMessage(IWebSocketConnection socket, string message)
        {
            //Console.WriteLine("ReceivedMessage: " + message);
            messageHandler.HandleMessage(socket, message);
        }

    }
}
