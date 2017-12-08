using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class PlayerRemoveComposer : ServerMessage
    {
        public PlayerRemoveComposer(int userId) : base(ServerOpCodes.PLAYER_REMOVE)
        {
            AppendInt(userId);
        }
    }
}
