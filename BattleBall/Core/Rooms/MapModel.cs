namespace BattleBall.Core.Rooms
{
    class MapModel
    {
        public int MaxX = 9;
        public int MaxY = 13;
        public int TSize = 64;
        public int DoorX = 0;
        public int DoorY = 4;
        public double DoorZ = 0.0;
        public int DoorRot = 2;
        public int[,] Map;

        public MapModel()
        {
            Map = new int[MaxX, MaxY];
            for (int i = 1; i < MaxX; i++)
            {
                for (int j = 0; j < MaxY; j++)
                {
                    Map[i, j] = 1;
                }
            }
            Map[DoorX, DoorY] = 1;
        }
    }
}
