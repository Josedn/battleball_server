using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class PlayerMovementComposer : ServerMessage
    {
        public PlayerMovementComposer(int userId, int x, int y, int rot) : base(ServerOpCodes.PLAYER_MOVEMENT)
        {
            AppendInt(userId);
            AppendInt(x);
            AppendInt(y);
            AppendInt(rot);
        }
    }
}
