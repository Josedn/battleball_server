using Fleck;
using BattleBall.Core.GameClients.Messages;
using BattleBall.Misc;
using System;

namespace BattleBall.Core.GameClients
{
    class GameClient
    {
        private IWebSocketConnection Connection;
        private GameClientMessageHandler MessageHandler;

        internal GameClient(IWebSocketConnection Connection)
        {
            this.Connection = Connection;
            this.MessageHandler = new GameClientMessageHandler(this);
        }

        internal void HandleMessage(string RawMessage)
        {
            Logging.WriteLine("RawMessage: '" + RawMessage + "'", ConsoleColor.Cyan);
            ClientMessage Message = new ClientMessage(RawMessage);
            Logging.WriteLine("Id: '" + Message.Id + "'", ConsoleColor.Cyan);

            this.MessageHandler.HandleMessage(Message);
        }

        internal void SendMessage(ServerMessage Message)
        {
            Connection.Send(Message.ToString());
        }
    }
}
