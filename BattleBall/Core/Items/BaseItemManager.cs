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

        public BaseItem AddItem(ItemType type, int baseId, int x, int y, double z, string itemName, int states, bool stackable, bool walkable, bool isSeat, List<int> directions)
        {
            if (!Items.ContainsKey(baseId))
            {
                Items[baseId] = new BaseItem(type, baseId, x, y, z, itemName, states, stackable, walkable, isSeat, directions);
            }
            return Items[baseId];
        }

        public BaseItem GetItem(int itemId)
        {
            if (Items.ContainsKey(itemId))
                return Items[itemId];
            return null;
        }


    }
}
