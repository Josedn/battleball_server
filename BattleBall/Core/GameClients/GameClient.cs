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
        internal User User;

        internal GameClient(IWebSocketConnection Connection)
        {
            this.Connection = Connection;
            this.MessageHandler = new GameClientMessageHandler(this);
            this.User = null;
        }

        internal void HandleMessage(string RawMessage)
        {
            Logging.WriteLine("RawMessage: '" + RawMessage + "'", ConsoleColor.Cyan);
            ClientMessage Message = new ClientMessage(RawMessage);
            Logging.WriteLine("Id: '" + Message.Id + "'", ConsoleColor.Cyan);

            this.MessageHandler.HandleMessage(Message);
        }

        internal void Stop()
        {
            if (User != null)
            {
                User.OnDisconnect();
            }

            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }

            if (MessageHandler != null)
            {
                MessageHandler.Destroy();
                MessageHandler = null;
            }
        }

        internal void SendMessage(ServerMessage Message)
        {
            Connection.Send(Message.ToString());
        }
    }
}
