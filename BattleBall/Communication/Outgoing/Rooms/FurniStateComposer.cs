using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class FurniStateComposer : ServerMessage
    {
        public FurniStateComposer(int furniId, int state) : base(ServerOpCodes.FURNI_STATE)
        {
            AppendInt(furniId);
            AppendInt(state);
        }
    }
}
