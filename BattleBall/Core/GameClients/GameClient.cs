using Fleck;
using BattleBall.Communication.Protocol;
using BattleBall.Misc;
using System;

namespace BattleBall.Core.GameClients
{
    class GameClient
    {
        private IWebSocketConnection Connection;
        internal GameClientMessageHandler MessageHandler;
        internal User User;

        internal GameClient(IWebSocketConnection connection, GameClientMessageHandler messageHandler)
        {
            Connection = connection;
            MessageHandler = messageHandler;
            User = null;
        }

        internal void HandleMessage(string RawMessage)
        {
            Logging.WriteLine("RawMessage: '" + RawMessage + "'", ConsoleColor.Cyan, Misc.LogLevel.SuperDebug);
            ClientMessage Message = new ClientMessage(RawMessage);

            MessageHandler.HandleMessage(this, Message);
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
        }

        internal void SendMessage(ServerMessage Message)
        {
            Logging.WriteLine("Sent: " + Message.ToString(), ConsoleColor.Yellow, Misc.LogLevel.SuperDebug);
            if (Connection.IsAvailable)
                Connection.Send(Message.ToString());
            else
                Stop();
        }
    }
}
