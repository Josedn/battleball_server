using System;
using BattleBall.Misc;

namespace BattleBall.Core.GameClients.Messages
{
    class GameClientMessageHandler
    {
        private GameClient Session;
        private ClientMessage Request;

        private delegate void RequestHandler();
        private RequestHandler[] RequestHandlers;

        private const int HIGHEST_MESSAGE_ID = 10;

        internal GameClientMessageHandler(GameClient Session)
        {
            this.Session = Session;
            this.RequestHandlers = new RequestHandler[HIGHEST_MESSAGE_ID];
            RegisterRequests();
        }

        internal void RequestMap()
        {
            Logging.WriteLine("Sending map to " + Session.ToString(), System.ConsoleColor.Green);
            ServerMessage response = new ServerMessage(ServerOpCodes.MAP_DATA);

            response.AppendInt(BattleEnvironment.Game.MapModel.Width);
            response.AppendInt(BattleEnvironment.Game.MapModel.Height);



            Session.SendMessage(response);
        }

        internal void Login()
        {
            string username = Request.PopString();
            Logging.WriteLine(username + " has logged in!", ConsoleColor.Green);

            Session.SendMessage(new ServerMessage(ServerOpCodes.LOGIN_OK));
        }

        internal void HandleMessage(ClientMessage Message)
        {
            if (Message.Id < 0 || Message.Id > HIGHEST_MESSAGE_ID)
            {
                Logging.WriteLine("MessageId out of protocol request.", ConsoleColor.Red);
                return;
            }

            if (RequestHandlers[Message.Id] == null)
            {
                Logging.WriteLine("No handler for id: " + Message.Id, ConsoleColor.Red);
                return;
            }

            Request = Message;
            RequestHandlers[Message.Id].Invoke();
            Request = null;
        }

        private void RegisterRequests()
        {
            RequestHandlers[ClientOpCodes.REQUEST_MAP] = RequestMap;
            RequestHandlers[ClientOpCodes.LOGIN] = Login;
        }
    }
}
