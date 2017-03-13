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
            lock (this.Clients)
            {
                this.Clients.Remove(socket);
            }
            Logging.WriteLine("Client disconnected", ConsoleColor.Red);
        }

        public void HandleMessage(IWebSocketConnection socket, string message)
        {
            try
            {
                lock (this.Clients)
                {
                    this.Clients[socket].HandleMessage(message);
                }                    
            }
            catch (KeyNotFoundException)
            {
                Logging.WriteLine("Client key not found", ConsoleColor.Red);
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

        public void BroadcastMessage(ServerMessage Message)
        {
            foreach (GameClient client in Clients.Values)
            {
                client.SendMessage(Message);
            }
        }
    }
}
