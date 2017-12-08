using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestLookAt : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            int userId = request.PopInt();

            RoomUser user = session.User.CurrentRoomUser;
            if (user != null)
                user.LookAt(userId);
        }
    }
}
