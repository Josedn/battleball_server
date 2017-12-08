using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestMovement : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            int x = request.PopInt();
            int y = request.PopInt();

            RoomUser user = session.User.CurrentRoomUser;
            user.MoveTo(x, y);
        }
    }
}
