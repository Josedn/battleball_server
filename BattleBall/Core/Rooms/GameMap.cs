using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms
{
    class GameMap
    {
        private Room Room;
        private Hashtable CoordinatedUsers;
        private Hashtable CoordinatedItems;
        private bool DiagonalEnabled;

        public GameMap(Room room, bool enableDiagonal)
        {
            Room = room;
            CoordinatedItems = new Hashtable();
            CoordinatedUsers = new Hashtable();
            DiagonalEnabled = enableDiagonal;
        }

    }
}
