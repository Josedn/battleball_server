using Fleck;
using System;
using System.Collections.Generic;
using BattleBall.Misc;
using BattleBall.Net;
using BattleBall.Core.GameClients.Messages;

namespace BattleBall.Core.GameClients
{
    class GameClientManager : ISocketHandler
    {
        public Dictionary<IWebSocketConnection, GameClient> Clients;

        public GameClientManager()
        {
            this.Clients = new Dictionary<IWebSocketConnection, GameClient>();
        }

        public void HandleDisconnect(IWebSocketConnection socket)
        {
            // ?
        }

        public void HandleMessage(IWebSocketConnection socket, string message)
        {
            if (Clients[socket] != null)
            {
                Clients[socket].HandleMessage(message); 
            }
        }

        public void HandleStartClient(IWebSocketConnection socket)
        {
            Logging.WriteLine("Starting gameclient...", ConsoleColor.Yellow);
            lock (this.Clients)
            {
                Clients.Add(socket, new GameClient(socket));
            }
        }
    }
}
