using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class ChatComposer : ServerMessage
    {
        public ChatComposer(int userId, string chat) : base(ServerOpCodes.CHAT)
        {
            AppendInt(userId);
            AppendString(chat);
        }
    }
}
