using System;
using BattleBall.Misc;
using BattleBall.Core.Rooms;

namespace BattleBall.Core.GameClients.Messages
{
    class GameClientMessageHandler
    {
        #region Fields
        private GameClient Session;
        private ClientMessage Request;

        private delegate void RequestHandler();
        private RequestHandler[] RequestHandlers;

        private const int HIGHEST_MESSAGE_ID = 10;

        private static int NextId = 0;
        #endregion

        #region Constructor
        internal GameClientMessageHandler(GameClient Session)
        {
            this.Session = Session;
            this.RequestHandlers = new RequestHandler[HIGHEST_MESSAGE_ID];
            RegisterRequests();
        }
        #endregion

        #region Methods
        internal void RequestChat()
        {
            string Message = Request.PopString();
            RoomUser user = Session.User.CurrentRoom.GetRoomUserByUserId(Session.User.Id);
            user.Chat(Message);

            Logging.WriteLine(Session.User.Username + ": " + Message, ConsoleColor.Yellow);
        }
        internal void RequestMap()
        {
            Logging.WriteLine("Sending map to " + Session.User.Username, ConsoleColor.Green);
            ServerMessage response = new ServerMessage(ServerOpCodes.MAP_DATA);

            MapModel model = BattleEnvironment.Game.MapModel;

            response.AppendInt(model.Width);
            response.AppendInt(model.Height);

            for (int i = 0; i < model.Width; i++)
            {
                for (int j = 0; j < model.Height; j++)
                {
                    response.AppendInt(model.Layer[i, j]);
                }
            }
            Session.SendMessage(response);

            BattleEnvironment.Game.Room.AddPlayerToRoom(Session);
        }

        internal void Destroy()
        {
            Session = null;
            RequestHandlers = null;
            Request = null;
        }

        internal void Login()
        {
            string username = Request.PopString();
            string look = Request.PopString();

            if (Session.User == null)
            {
                Session.User = new User(NextId++, username, look, Session);
                Logging.WriteLine(username + " (" + Session.User.Id + ") has logged in!", ConsoleColor.Green);

                Session.SendMessage(new ServerMessage(ServerOpCodes.LOGIN_OK));
            }
            else
            {
                Logging.WriteLine("Client already logged!", ConsoleColor.Red);
                Session.Stop();
            }
        }

        internal void RequestMovement()
        {
            int x = Request.PopInt();
            int y = Request.PopInt();

            RoomUser user = Session.User.CurrentRoom.GetRoomUserByUserId(Session.User.Id);
            user.MoveTo(x, y);

            Logging.WriteLine(Session.User.Username + " wants to move to " + x + ", " + y, ConsoleColor.Yellow);
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
            RequestHandlers[ClientOpCodes.REQUEST_MOVEMENT] = RequestMovement;
            RequestHandlers[ClientOpCodes.REQUEST_CHAT] = RequestChat;
        }
        #endregion
    }
}
