using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestWave : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            RoomUser user = session.User.CurrentRoomUser;
            if (user != null)
                user.Wave();
        }
    }
}
