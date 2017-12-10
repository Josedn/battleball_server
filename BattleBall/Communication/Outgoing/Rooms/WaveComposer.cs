using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    internal class WaveComposer : ServerMessage
    {
        public WaveComposer(int userId) : base(ServerOpCodes.PLAYER_WAVE)
        {
            AppendInt(userId);
        }
    }
}