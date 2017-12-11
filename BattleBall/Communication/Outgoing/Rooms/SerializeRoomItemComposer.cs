using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                AppendString(item.Z.ToString());
                AppendInt(item.Rot);
                AppendInt(item.BaseItem.BaseId);
            }
        }

    }
}
