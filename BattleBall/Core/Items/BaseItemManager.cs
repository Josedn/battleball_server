using System.Collections.Generic;

namespace BattleBall.Core.Items
{
    class BaseItemManager
    {
        private Dictionary<int, BaseItem> Items;

        public BaseItemManager()
        {
            Items = new Dictionary<int, BaseItem>();
        }

        public BaseItem AddRoomItem(int id, int baseId, int x, int y, double z, string itemName, int states, bool stackable, bool walkable, bool isSeat, List<int> directions)
        {
            if (!Items.ContainsKey(id))
            {
                Items[id] = new BaseItem(id, ItemType.RoomItem, baseId, x, y, z, itemName, states, stackable, walkable, isSeat, directions);
            }
            return Items[id];
        }

        public BaseItem AddWallItem(int id, int baseId, string itemName, int states)
        {
            if (!Items.ContainsKey(id))
            {
                Items[id] = new BaseItem(id, ItemType.WallItem, baseId, 0, 0, 0, itemName, states, false, false, false, new List<int>() { 2, 4 });
            }
            return Items[id];
        }

        public BaseItem GetItem(int id)
        {
            if (Items.ContainsKey(id))
                return Items[id];
            return null;
        }

        public BaseItem FindItem(string itemName)
        {
            foreach (BaseItem item in Items.Values)
            {
                if (itemName.ToLower() == item.ItemName.ToLower())
                {
                    return item;
                }
            }
            return null;
        }


    }
}
