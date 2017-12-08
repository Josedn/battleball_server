using BattleBall.Communication.Protocol;
using BattleBall.Core.GameClients;

namespace BattleBall.Communication.Incoming
{
    interface IncomingEvent
    {
        void Handle(GameClient session, ClientMessage request);
    }
}
