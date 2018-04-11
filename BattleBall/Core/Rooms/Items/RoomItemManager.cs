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
        private Room Room;

        public RoomItemManager(Room room)
        {
            Room = room;
            RoomItems = new Dictionary<int, RoomItem>();
        }

        internal void AddRoomItemToRoom(int itemId, int x, int y, double z, int rot, BaseItem baseItem)
        {
            if (!RoomItems.ContainsKey(itemId))
            {
                RoomItems[itemId] = new RoomItem(itemId, x, y, z, rot, Room, baseItem);
            }
        }

        public RoomItem GetItem(int itemId)
        {
            if (RoomItems.ContainsKey(itemId))
                return RoomItems[itemId];
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
