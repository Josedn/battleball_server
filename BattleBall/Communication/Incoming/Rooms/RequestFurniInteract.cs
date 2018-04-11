using System;
using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestFurniInteract : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            int itemId = request.PopInt();

            RoomUser user = session.User.CurrentRoomUser;
            if (user != null)
                user.FurniInteract(itemId);
        }
    }
}
