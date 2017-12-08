//using System;
//using BattleBall.Misc;
//using BattleBall.Core.Rooms;
//using BattleBall.Communication.Protocol;
//using BattleBall.Communication.Outgoing.Rooms;

//namespace BattleBall.Core.GameClients
//{
//    class OldGameClientMessageHandler
//    {
//        #region Fields
//        private GameClient Session;
//        private ClientMessage Request;

//        private delegate void RequestHandler();
//        private RequestHandler[] RequestHandlers;

//        private const int HIGHEST_MESSAGE_ID = 11;

//        #endregion

//        #region Constructor
//        internal OldGameClientMessageHandler(GameClient Session)
//        {
//            this.Session = Session;
//            this.RequestHandlers = new RequestHandler[HIGHEST_MESSAGE_ID];
//            RegisterRequests();
//        }
//        #endregion

//        #region Methods
//        internal void RequestChat()
//        {
//            string Message = Request.PopString();
//            RoomUser user = Session.User.CurrentRoom.GetRoomUserByUserId(Session.User.Id);
//            user.Chat(Message);

//            Logging.WriteLine(Session.User.Username + ": " + Message, ConsoleColor.Yellow);
//        }

//        internal void RequestMap()
//        {
            
//        }

//        internal void Destroy()
//        {
//            Session = null;
//            RequestHandlers = null;
//            Request = null;
//        }

//        internal void Login()
//        {
            
//        }

//        internal void RequestMovement()
//        {
            
//        }

//        internal void HandleMessage(ClientMessage Message)
//        {
//            if (Message.Id < 0 || Message.Id > HIGHEST_MESSAGE_ID)
//            {
//                Logging.WriteLine("MessageId out of protocol request.", ConsoleColor.Red);
//                return;
//            }

//            if (RequestHandlers[Message.Id] == null)
//            {
//                Logging.WriteLine("No handler for id: " + Message.Id, ConsoleColor.Red);
//                return;
//            }

//            Request = Message;
//            RequestHandlers[Message.Id].Invoke();
//            Request = null;
//        }

//        private void RegisterRequests()
//        {
//            RequestHandlers[ClientOpCodes.REQUEST_MAP] = RequestMap;
//            RequestHandlers[ClientOpCodes.LOGIN] = Login;
//            RequestHandlers[ClientOpCodes.REQUEST_MOVEMENT] = RequestMovement;
//            RequestHandlers[ClientOpCodes.REQUEST_CHAT] = RequestChat;
//        }
//        #endregion
//    }
//}
