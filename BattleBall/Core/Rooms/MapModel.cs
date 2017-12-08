namespace BattleBall.Core.Rooms
{
    class MapModel
    {
        public int Cols = 9;
        public int Rows = 13;
        public int TSize = 64;
        public int DoorX = 0;
        public int DoorY = 4;
        public int[,] Layer
        {
            get
            {
                int[,] matrix = new int[Cols, Rows];
                for (int i = 1; i < Cols; i++)
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        matrix[i, j] = 1;
                    }
                }
                matrix[DoorX, DoorY] = 1;
                return matrix;
            }
        }
        /*public int[][][] Layers = new int[][][] {
            new int[][] {
                new int[]{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 3 },
                new int[]{ 3, 3, 3, 1, 1, 2, 3, 3, 3, 3, 3, 3 }
            },
            new int[][] {
                new int[]{ 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                new int[]{ 4, 4, 4, 0, 5, 4, 4, 4, 4, 4, 4, 4 },
                new int[]{ 4, 4, 4, 0, 0, 3, 3, 3, 3, 3, 3, 3 }
            }
        };*/
    }
}
