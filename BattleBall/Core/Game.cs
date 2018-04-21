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

            BaseItem shelves_norja = ItemManager.AddItem(ItemType.RoomItem, 13, 1, 1, 0, "shelves_norja", 1, false, false, false, new List<int>() { 0, 2 });
            BaseItem rare_dragon = ItemManager.AddItem(ItemType.RoomItem, 1620, 1, 1, 0, "rare_dragonlamp*0", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem hologram = ItemManager.AddItem(ItemType.RoomItem, 234, 1, 1, 0, "hologram", 2, false, false, false, new List<int>() { 0 });
            BaseItem club_sofa = ItemManager.AddItem(ItemType.RoomItem, 267, 2, 1, 0, "club_sofa", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem doorD = ItemManager.AddItem(ItemType.RoomItem, 1505, 1, 1, 0, "doorD", 3, false, true, false, new List<int>() { 2, 4 });
            BaseItem tile_brown = ItemManager.AddItem(ItemType.RoomItem, 2582, 1, 1, 0.1, "tile_brown", 1, false, true, false, new List<int>() { 0 });
            BaseItem tile_marble = ItemManager.AddItem(ItemType.RoomItem, 2566, 1, 1, 0.1, "tile_marble", 1, false, true, false, new List<int>() { 0 });

            Authenticator = new Authenticator(this);
            Room = new Room(new MapModel());
            double z = 0;
            Room.RoomItemManager.AddRoomItemToRoom(1, 3, 3, z, shelves_norja.Directions[0], shelves_norja);
            Room.RoomItemManager.AddRoomItemToRoom(2, 6, 2, z, rare_dragon.Directions[1], rare_dragon);
            Room.RoomItemManager.AddRoomItemToRoom(3, 3, 8, z, hologram.Directions[0], hologram);
            Room.RoomItemManager.AddRoomItemToRoom(4, 6, 6, z, club_sofa.Directions[1], club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(5, 6, 8, z, club_sofa.Directions[1], club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(6, 1, 9, z, doorD.Directions[0], doorD);
            /*Room.RoomItemManager.AddRoomItemToRoom(7, 1, 0, z, doorD.Directions[1], doorD);
            int itemId = 8;
            for (int i = 1; i < Room.Model.Cols; i++)
            {
                for (int j = 0; j < Room.Model.Rows; j++)
                {
                    Room.RoomItemManager.AddRoomItemToRoom(itemId++, i, j, 0, 0, i % 2 == 0 ? tile_brown : tile_marble);
                }
            }
            for (int i = 1; i < Room.Model.Cols; i++)
            {
                for (int j = 0; j < Room.Model.Rows; j++)
                {
                    Room.RoomItemManager.AddRoomItemToRoom(itemId++, i, j, 0.1, 0, i % 2 == 0 ? tile_brown : tile_marble);
                }
            }
            */

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
