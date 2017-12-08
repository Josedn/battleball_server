using BattleBall.Communication.Protocol;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class LoginOkComposer : ServerMessage
    {
        public LoginOkComposer() : base(ServerOpCodes.LOGIN_OK)
        {

        }
    }
}
