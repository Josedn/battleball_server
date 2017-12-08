using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestChat : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            string chat = request.PopString();

            RoomUser user = session.User.CurrentRoomUser;
            user.Chat(chat);
        }
    }
}
