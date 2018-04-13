using BattleBall.Communication.Incoming;
using BattleBall.Communication.Incoming.Rooms;
using BattleBall.Communication.Protocol;
using BattleBall.Misc;
using System;

namespace BattleBall.Core.GameClients
{
    class GameClientMessageHandler
    {
        private IncomingEvent[] RequestHandlers;
        private const int HIGHEST_MESSAGE_ID = 20;

        public GameClientMessageHandler()
        {
            RequestHandlers = new IncomingEvent[HIGHEST_MESSAGE_ID];
            RegisterRequests();
        }

        public bool HandleMessage(GameClient session, ClientMessage message)
        {
            if (message.Id < 0 || message.Id > HIGHEST_MESSAGE_ID)
            {
                Logging.WriteLine("MessageId out of protocol request.", ConsoleColor.Red, LogLevel.Debug);
                return false;
            }

            if (RequestHandlers[message.Id] == null)
            {
                Logging.WriteLine("No handler for id: " + message.Id, ConsoleColor.Red, LogLevel.Debug);
                return false;
            }
            Logging.WriteLine("Handled by: '" + RequestHandlers[message.Id].GetType().Name + "'", ConsoleColor.Cyan, LogLevel.Debug);
            RequestHandlers[message.Id].Handle(session, message);
            return true;
        }

        private void RegisterRequests()
        {
            RequestHandlers[ClientOpCodes.REQUEST_MAP] = new RequestMap();
            RequestHandlers[ClientOpCodes.LOGIN] = new Login();
            RequestHandlers[ClientOpCodes.REQUEST_MOVEMENT] = new RequestMovement();
            RequestHandlers[ClientOpCodes.REQUEST_CHAT] = new RequestChat();
            RequestHandlers[ClientOpCodes.REQUEST_LOOK_AT] = new RequestLookAt();
            RequestHandlers[ClientOpCodes.REQUEST_WAVE] = new RequestWave();
            RequestHandlers[ClientOpCodes.REQUEST_ROOM_DATA] = new RequestRoomData();
            RequestHandlers[ClientOpCodes.REQUEST_FURNI_INTERACT] = new RequestFurniInteract();
        }
    }
}
