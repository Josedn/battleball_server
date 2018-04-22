using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms.Items;
using System.Collections.Generic;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class SerializeWallItemComposer : ServerMessage
    {
        public SerializeWallItemComposer(WallItem item) : base(ServerOpCodes.WALL_ITEM_DATA)
        {
            CreateMessage(new List<WallItem>
            {
                item
            });
        }

        public SerializeWallItemComposer(ICollection<WallItem> item) : base(ServerOpCodes.WALL_ITEM_DATA)
        {
            CreateMessage(item);
        }

        private void CreateMessage(ICollection<WallItem> items)
        {
            AppendInt(items.Count);
            foreach (WallItem item in items)
            {
                AppendInt(item.ItemId);
                AppendInt(item.X);
                AppendInt(item.Y);
                AppendInt(item.Rot);
                AppendInt(item.BaseItem.BaseId);
                AppendInt(item.State);
            }
        }

    }
}
