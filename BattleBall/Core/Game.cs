using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
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

            int baseItemId = 0;

            BaseItem shelves_norja = ItemManager.AddRoomItem(baseItemId++, 13, 1, 1, 0, "shelves_norja", 1, false, false, false, new List<int>() { 0, 2 });
            BaseItem rare_dragon = ItemManager.AddRoomItem(baseItemId++, 1620, 1, 1, 0, "rare_dragonlamp*0", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem hologram = ItemManager.AddRoomItem(baseItemId++, 234, 1, 1, 0, "hologram", 2, false, false, false, new List<int>() { 0 });
            BaseItem club_sofa = ItemManager.AddRoomItem(baseItemId++, 267, 2, 1, 0, "club_sofa", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem doorD = ItemManager.AddRoomItem(baseItemId++, 1505, 1, 1, 0, "doorD", 3, false, true, false, new List<int>() { 2, 4 });
            BaseItem tile_brown = ItemManager.AddRoomItem(baseItemId++, 2582, 1, 1, 0.15, "tile_brown", 1, false, true, false, new List<int>() { 0 });
            BaseItem tile_marble = ItemManager.AddRoomItem(baseItemId++, 2566, 1, 1, 0.15, "tile_marble", 1, false, true, false, new List<int>() { 0 });

            BaseItem hc_wall_lamp = ItemManager.AddWallItem(baseItemId++, 4003, "hc_wall_lamp", 2);
            BaseItem flag_mexico = ItemManager.AddWallItem(baseItemId++, 4250, "flag_mexico", 2);
            BaseItem flag_columbia = ItemManager.AddWallItem(baseItemId++, 4258, "flag_columbia", 1);

            Authenticator = new Authenticator(this);
            Room = new Room(new MapModel());
            double z = 0.15;
            int itemId = 0;
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 3, z, shelves_norja.Directions[0], shelves_norja);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 2, z, rare_dragon.Directions[1], rare_dragon);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 8, z, hologram.Directions[0], hologram);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 6, z, club_sofa.Directions[1], club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 8, z, club_sofa.Directions[1], club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 10, z, doorD.Directions[0], doorD);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 0, z, doorD.Directions[1], doorD);

            Room.RoomItemManager.AddWallItemToRoom(itemId++, 160, 0, 4, hc_wall_lamp);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -190, 59, 2, flag_mexico);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130, 30, 2, flag_columbia);

            for (int i = 1; i < Room.Model.Cols; i++)
            {
                for (int j = 0; j < Room.Model.Rows; j++)
                {
                    Room.RoomItemManager.AddRoomItemToRoom(itemId++, i, j, 0, 0, i % 2 == 0 ? tile_brown : tile_marble);
                }
            }            

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
