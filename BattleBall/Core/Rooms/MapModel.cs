using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms
{
    class MapModel
    {
        public int Height = 10;
        public int Width = 10;
        public int[][][] Layers = new int[][][] {
            new int[][] {
                new int[]{ 3, 3, 3, 3, 3, 3, 3, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 2, 1, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 3, 1, 3 },
                new int[]{ 3, 1, 1, 2, 1, 1, 1, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 2, 1, 1, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 2, 1, 1, 3, 1, 3 },
                new int[]{ 3, 3, 3, 1, 2, 3, 3, 3, 1, 3 },
                new int[]{ 3, 1, 1, 1, 2, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 2, 1, 1, 1, 1, 3 }
            },
            new int[][] {
                new int[]{ 4, 3, 3, 3, 3, 3, 3, 4, 1, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 4, 1, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 4, 1, 4 },
                new int[]{ 4, 0, 0, 5, 0, 0, 0, 4, 1, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 4, 1, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 4, 1, 4 },
                new int[]{ 4, 4, 4, 0, 5, 4, 4, 4, 1, 4 },
                new int[]{ 4, 3, 3, 0, 0, 3, 3, 3, 1, 4 },
                new int[]{ 4, 1, 1, 0, 0, 0, 0, 0, 1, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 1, 4 }
            }
        };
    }
}
