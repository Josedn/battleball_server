using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class FurniRemoveComposer : ServerMessage
    {
        public FurniRemoveComposer(int furniId) : base(ServerOpCodes.ITEM_REMOVE)
        {
            AppendInt(furniId);
        }
    }
}
