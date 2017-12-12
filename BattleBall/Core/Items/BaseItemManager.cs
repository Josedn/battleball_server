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

        public void AddItem(int baseId, int x, int y, double z, string assetName)
        {
            if (!Items.ContainsKey(baseId))
            {
                Items[baseId] = new BaseItem(baseId, x, y, z, assetName);
            }
        }

        public BaseItem GetItem(int itemId)
        {
            if (Items.ContainsKey(itemId))
                return Items[itemId];
            return null;
        }


    }
}
