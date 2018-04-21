using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms.Items;
using System.Collections.Generic;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class SerializeRoomItemComposer : ServerMessage
    {
        public SerializeRoomItemComposer(RoomItem item) : base(ServerOpCodes.FURNI_DATA)
        {
            CreateMessage(new List<RoomItem>
            {
                item
            });
        }

        public SerializeRoomItemComposer(ICollection<RoomItem> item) : base(ServerOpCodes.FURNI_DATA)
        {
            CreateMessage(item);
        }

        private void CreateMessage(ICollection<RoomItem> items)
        {
            AppendInt(items.Count);
            foreach (RoomItem item in items)
            {
                AppendInt(item.ItemId);
                AppendInt(item.X);
                AppendInt(item.Y);
                AppendFloat(item.Z);
                AppendInt(item.Rot);
                AppendInt(item.BaseItem.BaseId);
                AppendInt(item.State);
            }
        }

    }
}
