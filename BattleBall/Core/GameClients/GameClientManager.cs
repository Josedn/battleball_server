using Fleck;
using System;
using System.Collections.Generic;
using BattleBall.Misc;
using BattleBall.Net;
using BattleBall.Communication.Protocol;

namespace BattleBall.Core.GameClients
{
    class GameClientManager : ISocketHandler
    {
        public Dictionary<Guid, GameClient> Clients;
        public GameClientMessageHandler MessageHandler;

        public GameClientManager()
        {
            this.Clients = new Dictionary<Guid, GameClient>();
            this.MessageHandler = new GameClientMessageHandler();
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
                    Logging.WriteLine("Client key not found " + socket.ConnectionInfo.Id, ConsoleColor.Red, Misc.LogLevel.Warning);
                }
            }
        }

        public void HandleStartClient(IWebSocketConnection socket)
        {
            lock (this.Clients)
            {
                Clients.Add(socket.ConnectionInfo.Id, new GameClient(socket, MessageHandler));
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
