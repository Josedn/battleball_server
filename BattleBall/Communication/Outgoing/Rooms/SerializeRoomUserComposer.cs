using System.Collections.Generic;
using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class SerializeRoomUserComposer : ServerMessage
    {
        public SerializeRoomUserComposer(ICollection<RoomUser> users) : base(ServerOpCodes.PLAYERS_DATA)
        {
            CreateMessage(users);
        }

        public SerializeRoomUserComposer(RoomUser user) : base(ServerOpCodes.PLAYERS_DATA)
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
                AppendString(Player.User.Username);
                AppendString(Player.User.Look);
            }
        }
    }
}
