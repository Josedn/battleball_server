using System;
using System.Threading;
using System.Threading.Tasks;
using BattleBall.Net;
using BattleBall.Core.Rooms;
using BattleBall.Core.GameClients;
using BattleBall.Core.Items;

namespace BattleBall.Core
{
    internal class Game
    {
        #region Fields
        public Room Room;
        internal ConnectionManager ConnectionManager;
        internal GameClientManager ClientManager;
        internal Authenticator Authenticator;
        internal BaseItemManager ItemManager;
        private const int DELTA_TIME = 500;
        #endregion

        #region Constructor
        internal Game()
        {
            ClientManager = new GameClientManager();
            ConnectionManager = new ConnectionManager(ClientManager, 443);

            ItemManager = new BaseItemManager();

            BaseItem shelves_norja = ItemManager.AddItem(ItemType.RoomItem, 13, 1, 1, 0, "shelves_norja", 1, false, false, false, new System.Collections.Generic.List<int>() { 0, 2 });
            BaseItem rare_dragon = ItemManager.AddItem(ItemType.RoomItem, 1620, 1, 1, 0, "rare_dragonlamp*0", 2, false, false, false, new System.Collections.Generic.List<int>() { 2, 4 });
            BaseItem hologram = ItemManager.AddItem(ItemType.RoomItem, 234, 1, 1, 0, "hologram", 2, false, false, false, new System.Collections.Generic.List<int>() { 0 });
            BaseItem club_sofa = ItemManager.AddItem(ItemType.RoomItem, 267, 2, 1, 0, "club_sofa", 1, false, false, true, new System.Collections.Generic.List<int>() { 0, 2, 4, 6 });

            Authenticator = new Authenticator(this);
            Room = new Room(new MapModel());

            Room.RoomItemManager.AddRoomItemToRoom(1, 3, 3, 0, shelves_norja.Directions[0], shelves_norja);
            Room.RoomItemManager.AddRoomItemToRoom(2, 6, 2, 0, rare_dragon.Directions[1], rare_dragon);
            Room.RoomItemManager.AddRoomItemToRoom(3, 3, 8, 0, hologram.Directions[0], hologram);
            Room.RoomItemManager.AddRoomItemToRoom(4, 6, 6, 0, club_sofa.Directions[1], club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(5, 6, 8, 0, club_sofa.Directions[1], club_sofa);

            Task RoomThread = new Task(OnCycle);
            RoomThread.Start();
        }
        #endregion

        #region Methods
        internal void OnCycle()
        {
            while (true)
            {
                DateTime startTaskTime;
                TimeSpan spentTime;
                startTaskTime = DateTime.Now;

                Room.OnCycle();

                spentTime = DateTime.Now - startTaskTime;

                double sleepTime = DELTA_TIME - spentTime.TotalMilliseconds;

                if (sleepTime < 0)
                {
                    sleepTime = 0;
                }

                if (sleepTime > DELTA_TIME)
                {
                    sleepTime = DELTA_TIME;
                }

                Thread.Sleep((int)Math.Floor(sleepTime));
            }
        }
        #endregion
    }
}
