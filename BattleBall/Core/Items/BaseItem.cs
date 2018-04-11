using System.Collections.Generic;

namespace BattleBall.Core.Items
{
    class BaseItem
    {
        internal ItemType Type;
        internal int BaseId;
        internal int X;
        internal int Y;
        internal double Z;
        internal string ItemName;
        internal int States;
        internal bool Stackable;
        internal bool Walkable;
        internal bool IsSeat;
        internal List<int> Directions;

        public BaseItem(ItemType type, int baseId, int x, int y, double z, string itemName, int states, bool stackable, bool walkable, bool isSeat, List<int> directions)
        {
            Type = type;
            BaseId = baseId;
            X = x;
            Y = y;
            Z = z;
            ItemName = itemName;
            States = states;
            Stackable = stackable;
            Walkable = walkable;
            IsSeat = isSeat;
            Directions = directions;
        }
    }
}
