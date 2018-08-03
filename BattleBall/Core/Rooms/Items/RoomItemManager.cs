using BattleBall.Communication.Outgoing.Rooms;
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
                Room.GameMap.AddItemToMap(RoomItems[itemId]);
                Room.SendMessage(new SerializeRoomItemComposer(RoomItems[itemId]));
            }
        }

        internal void RemoveItem(int itemId)
        {
            RoomItem item = GetItem(itemId);
            if (item != null)
            {
                if (RoomItems.ContainsKey(itemId))
                {
                    RoomItems.Remove(itemId);
                    Room.GameMap.RemoveItemFromMap(item);
                }
                if (WallItems.ContainsKey(itemId))
                {
                    WallItems.Remove(itemId);
                }
                Room.SendMessage(new FurniRemoveComposer(itemId));
            }
        }

        internal void RemoveAllFurniture()
        {
            List<int> items = new List<int>();
            lock (RoomItems)
            {
                items.AddRange(RoomItems.Keys);
            }
            lock (WallItems)
            {
                items.AddRange(WallItems.Keys);
            }

            foreach (int itemId in items)
            {
                RemoveItem(itemId);
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
