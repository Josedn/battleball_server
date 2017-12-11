using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms
{
    class RoomItem
    {
        internal int ItemId;
        internal int X, Y, Rot;
        internal double Z;
        public Room Room { get; }

        public bool NeedsUpdate { get; set; }
        internal BaseItem BaseItem;

        public RoomItem(int itemId, int x, int y, double z, int rot, Room room, BaseItem baseItem)
        {
            ItemId = itemId;
            X = x;
            Y = y;
            Rot = rot;
            Z = z;
            Room = room;
            NeedsUpdate = false;
            BaseItem = baseItem;
        }
    }
}
