﻿using System;
using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;

namespace BattleBall.Communication.Incoming.Rooms
{
    class RequestMap : IncomingEvent
    {
        public void Handle(GameClient session, ClientMessage request)
        {
            BattleEnvironment.Game.Room.SendModelToPlayer(session);
        }
    }
}
