using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;

namespace BattleBall.Communication.Incoming.Rooms
{
    class Login : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            string username = request.PopString();
            string look = request.PopString();
            BattleEnvironment.Game.Authenticator.TryLogin(session, username, look);
        }
    }
}
