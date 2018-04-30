using BattleBall.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms.Items
{
    class RoomItemManager
    {
        internal Dictionary<int, RoomItem> RoomItems;
        internal Dictionary<int, WallItem> WallItems;
        private Room Room;

        public RoomItemManager(Room room)
        {
            Room = room;
            RoomItems = new Dictionary<int, RoomItem>();
            WallItems = new Dictionary<int, WallItem>();
        }

        internal void AddRoomItemToRoom(int itemId, int x, int y, double z, int rot, int state, BaseItem baseItem)
        {
            if (GetItem(itemId) == null)
            {
                RoomItems[itemId] = new RoomItem(itemId, x, y, z, rot, state, Room, baseItem);
            }
        }

        internal void AddWallItemToRoom(int itemId, int x, int y, int rot, int state, BaseItem baseItem)
        {
            if (GetItem(itemId) == null)
            {
                WallItems[itemId] = new WallItem(itemId, x, y, rot, state, Room, baseItem);
            }
        }

        public RoomItem GetItem(int itemId)
        {
            if (RoomItems.ContainsKey(itemId))
                return RoomItems[itemId];
            if (WallItems.ContainsKey(itemId))
                return WallItems[itemId];
            return null;
        }

        internal void FurniInteract(RoomUser roomUser, int itemId)
        {
            RoomItem item = GetItem(itemId);
            if (item != null)
            {
                item.Interactor.OnTrigger(roomUser, true);
            }
        }
    }
}
