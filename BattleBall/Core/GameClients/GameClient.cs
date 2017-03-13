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

        internal void BroadcastMovement()
        {
            ServerMessage response = new ServerMessage(ServerOpCodes.PLAYERS_DATA);
            response.AppendInt(BattleEnvironment.Game.Room.Players.Count);

            foreach (var Player in BattleEnvironment.Game.Room.Players)
            {
                response.AppendInt(Player.X);
                response.AppendInt(Player.Y);
            }

            SendMessage(response);
        }

        internal void SendMessage(ServerMessage Message)
        {
            Connection.Send(Message.ToString());
        }
    }
}
