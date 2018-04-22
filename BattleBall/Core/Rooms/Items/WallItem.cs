using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleBall.Core.Items;

namespace BattleBall.Core.Rooms.Items
{
    class WallItem : RoomItem
    {
        public WallItem(int itemId, int x, int y, int rot, Room room, BaseItem baseItem) : base(itemId, x, y, 0, rot, room, baseItem)
        {

        }
    }
}
