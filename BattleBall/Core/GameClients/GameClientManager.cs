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
        public Dictionary<Guid, GameClient> Clients;

        public GameClientManager()
        {
            this.Clients = new Dictionary<Guid, GameClient>();
        }

        public void HandleDisconnect(IWebSocketConnection socket)
        {
            this.Clients[socket.ConnectionInfo.Id].Stop();
            lock (this.Clients)
            {
                this.Clients.Remove(socket.ConnectionInfo.Id);
            }
        }

        public void HandleMessage(IWebSocketConnection socket, string message)
        {
            lock (this.Clients)
            {
                try
                {
                    this.Clients[socket.ConnectionInfo.Id].HandleMessage(message);
                }
                catch (KeyNotFoundException)
                {
                    Logging.WriteLine("Client key not found " + socket.ConnectionInfo.Id, ConsoleColor.Red);
                }
            }
        }

        public void HandleStartClient(IWebSocketConnection socket)
        {
            lock (this.Clients)
            {
                Clients.Add(socket.ConnectionInfo.Id, new GameClient(socket));
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
