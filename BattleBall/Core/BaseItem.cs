using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core
{
    class BaseItem
    {
        internal int BaseId;
        internal int X;
        internal int Y;
        internal double Z;

        public BaseItem(int baseId, int x, int y, double z)
        {
            BaseId = baseId;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
