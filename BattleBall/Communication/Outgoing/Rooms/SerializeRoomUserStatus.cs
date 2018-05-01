using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms;
using System.Collections.Generic;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class SerializeRoomUserStatus : ServerMessage
    {
        internal SerializeRoomUserStatus(ICollection<RoomUser> users) : base(ServerOpCodes.PLAYER_STATUS)
        {
            CreateMessage(users);
        }

        public SerializeRoomUserStatus(RoomUser user) : base(ServerOpCodes.PLAYER_STATUS)
        {
            CreateMessage(new List<RoomUser>
            {
                user
            });
        }

        private void CreateMessage(ICollection<RoomUser> users)
        {
            AppendInt(users.Count);

            foreach (RoomUser Player in users)
            {
                AppendInt(Player.UserId);
                AppendInt(Player.X);
                AppendInt(Player.Y);
                AppendFloat(Player.Z);
                AppendInt(Player.Rot);
                
                lock (Player.Statusses)
                {
                    AppendInt(Player.Statusses.Count);
                    foreach (KeyValuePair<string, string> status in Player.Statusses)
                    {
                        AppendString(status.Key);
                        AppendString(status.Value);
                    }
                }
            }
        }
    }
}
