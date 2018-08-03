using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BattleBall.Net;
using BattleBall.Core.Rooms;
using BattleBall.Core.GameClients;
using BattleBall.Core.Items;
using BattleBall.Misc;

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

        public static int baseItemId = 0;
        public static int itemId = 0;

        #region Constructor
        internal Game()
        {
            ClientManager = new GameClientManager();
            ConnectionManager = new ConnectionManager(ClientManager, 443);

            ItemManager = new BaseItemManager();

            Authenticator = new Authenticator(this);
            Room = new Room(new MapModel());
            
            
            double z = 0;

            #region Deep forest

            BaseItem club_sofa = ItemManager.AddRoomItem(baseItemId++, 267, 2, 1, 0, "club_sofa", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem lt_patch = ItemManager.AddRoomItem(baseItemId++, 3188, 2, 2, 0.01, "lt_patch", 3, false, true, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem lt_stone2 = ItemManager.AddRoomItem(baseItemId++, 3177, 2, 1, 1.05, "lt_stone2", 3, true, false, false, new List<int>() { 0, 2 });
            BaseItem lt_gate = ItemManager.AddRoomItem(baseItemId++, 3172, 2, 1, 0.01, "lt_gate", 2, false, true, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem scifidoor_4 = ItemManager.AddRoomItem(baseItemId++, 1575, 1, 1, 0, "scifidoor*4", 2, false, true, false, new List<int>() { 2, 4 });
            BaseItem hween12_scarecrow = ItemManager.AddRoomItem(baseItemId++, 4733, 1, 1, 0, "hween12_scarecrow", 2, false, false, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem rare_icecream_2 = ItemManager.AddRoomItem(baseItemId++, 1632, 1, 1, 0, "rare_icecream*2", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem rare_dragon_5 = ItemManager.AddRoomItem(baseItemId++, 1621, 1, 1, 0, "rare_dragonlamp*5", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem hween12_cart = ItemManager.AddRoomItem(baseItemId++, 4729, 1, 1, 0, "hween12_cart", 2, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem small_chair_armas = ItemManager.AddRoomItem(baseItemId++, 55, 1, 1, 0, "small_chair_armas", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem hween12_track = ItemManager.AddRoomItem(baseItemId++, 4731, 1, 1, 0.25, "hween12_track", 3, true, true, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem hween12_track_crl = ItemManager.AddRoomItem(baseItemId++, 4736, 1, 1, 0.25, "hween12_track_crl", 3, true, true, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem hween12_track_crr = ItemManager.AddRoomItem(baseItemId++, 4739, 1, 1, 0.25, "hween12_track_crr", 3, true, true, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem LT_skull = ItemManager.AddRoomItem(baseItemId++, 3189, 1, 1, 0.4, "LT_skull", 1, false, false, false, new List<int>() { 0, 2, 4, 6 });
            BaseItem hween12_moon = ItemManager.AddRoomItem(baseItemId++, 4740, 1, 1, 0.01, "hween12_moon", 4, false, true, false, new List<int>() { 2, 4 });

            BaseItem stories_shakespeare_tree = ItemManager.AddRoomItem(baseItemId++, 5735, 2, 2, 0, "stories_shakespeare_tree", 2, false, false, false, new List<int>() { 0, 2 });
            BaseItem anc_artifact3 = ItemManager.AddRoomItem(baseItemId++, 4655, 3, 1, 0, "anc_artifact3", 1, false, false, false, new List<int>() { 2, 4 });
            BaseItem anc_waterfall = ItemManager.AddRoomItem(baseItemId++, 4651, 1, 1, 0, "anc_waterfall", 1, false, true, false, new List<int>() { 2, 4 });
            BaseItem anc_talltree = ItemManager.AddRoomItem(baseItemId++, 4650, 2, 2, 0, "anc_talltree", 1, false, false, false, new List<int>() { 2, 4 });
            BaseItem anc_comfy_tree = ItemManager.AddRoomItem(baseItemId++, 4653, 1, 1, 0, "anc_comfy_tree", 1, false, false, false, new List<int>() { 0, 2, 4, 6 });

            BaseItem lt_jngl_wall = ItemManager.AddWallItem(baseItemId++, 4121, "lt_jngl_wall", 3);
            BaseItem anc_sunset_wall = ItemManager.AddWallItem(baseItemId++, 4462, "anc_sunset_wall", 2);

            BaseItem doorD = ItemManager.AddRoomItem(baseItemId++, 1505, 1, 1, 0, "doorD", 3, false, true, false, new List<int>() { 2, 4 });


            int currentX = 0;
            for (int i = 0; i < 4; i++)
            {
                int currentY = 0;
                for (int j = 0; j < 6; j++)
                {
                    Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1 + currentX, currentY, z, lt_patch.Directions[0], 1, lt_patch);
                    currentY += 2;
                }
                currentX += 2;
            }
            for (int i = 0; i < 4; i++)
            {
                Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1 + (i * 2), 11, z, lt_patch.Directions[0], 1, lt_patch);
            }


            z = lt_patch.Z;

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 12, z, scifidoor_4.Directions[0], 1, scifidoor_4);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 10, z, lt_stone2.Directions[1], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 8, z, lt_stone2.Directions[1], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 6, z, lt_stone2.Directions[1], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 4, z, lt_stone2.Directions[1], 0, lt_stone2);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 4, z, hween12_scarecrow.Directions[1], 1, hween12_scarecrow);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 3, z + 1.05, rare_icecream_2.Directions[1], 0, rare_icecream_2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 8, 12, z, rare_dragon_5.Directions[1], 1, rare_dragon_5);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 3, z, lt_stone2.Directions[0], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 3, z, lt_stone2.Directions[0], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 3, z, lt_stone2.Directions[0], 0, lt_stone2);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 3, z, lt_gate.Directions[0], 0, lt_gate);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 10, z, hween12_cart.Directions[1], 0, hween12_cart);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 8, z, hween12_cart.Directions[1], 0, hween12_cart);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 6, z, hween12_cart.Directions[1], 0, hween12_cart);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 4, z, hween12_cart.Directions[2], 0, hween12_cart);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 4, z, hween12_cart.Directions[2], 0, hween12_cart);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 8, 7, z, hween12_cart.Directions[3], 0, hween12_cart);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 8, 9, z, hween12_cart.Directions[3], 0, hween12_cart);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 12, z, hween12_cart.Directions[0], 0, hween12_cart);

            //
            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 0, z, club_sofa.Directions[2], 0, club_sofa);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 10, z, hween12_track.Directions[0], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 9, z, hween12_track.Directions[0], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 8, z, hween12_track.Directions[0], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 7, z, hween12_track.Directions[0], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 6, z, hween12_track.Directions[0], 0, hween12_track);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 10, z, hween12_track.Directions[2], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 9, z, hween12_track.Directions[2], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 8, z, hween12_track.Directions[2], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 7, z, hween12_track.Directions[2], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 6, z, hween12_track.Directions[2], 0, hween12_track);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 11, z, hween12_track.Directions[1], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 11, z, hween12_track.Directions[1], 0, hween12_track);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 5, z, hween12_track.Directions[3], 0, hween12_track);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 5, z, hween12_track.Directions[3], 0, hween12_track);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 5, z, hween12_track_crl.Directions[3], 0, hween12_track_crl);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 5, z, hween12_track_crl.Directions[3], 0, hween12_track_crl);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 11, z, hween12_track_crl.Directions[3], 0, hween12_track_crl);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 11, z, hween12_track_crl.Directions[3], 0, hween12_track_crl);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 8, z, LT_skull.Directions[2], 0, LT_skull);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 3, z, hween12_moon.Directions[0], 0, hween12_moon);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 0, z, stories_shakespeare_tree.Directions[0], 0, stories_shakespeare_tree);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 0, z, club_sofa.Directions[2], 0, club_sofa);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 0, z, anc_artifact3.Directions[0], 0, anc_artifact3);

            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 0, z, anc_waterfall.Directions[0], 0, anc_waterfall);
            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 1, z, anc_waterfall.Directions[0], 0, anc_waterfall);
            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 2, z, anc_waterfall.Directions[0], 0, anc_waterfall);

            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 0, z, anc_waterfall.Directions[1], 0, anc_waterfall);
            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 0, z, anc_waterfall.Directions[1], 0, anc_waterfall);
            //Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 0, z, anc_waterfall.Directions[1], 0, anc_waterfall);

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 0, z, doorD.Directions[1], 0, doorD);

            //Wall

            Room.RoomItemManager.AddWallItemToRoom(itemId++, -310, 155, 2, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -220, 115, 2, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130, 75, 2, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130 + 90, 75 - 40, 2, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 20, 75 - 40 - 20, 2, 0, lt_jngl_wall);

            Room.RoomItemManager.AddWallItemToRoom(itemId++, 110, 5, 4, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 195, 40, 4, 0, lt_jngl_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 280, 75, 4, 0, lt_jngl_wall);

            Room.RoomItemManager.AddWallItemToRoom(itemId++, -310, 155, 2, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -220, 115, 2, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130, 75, 2, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130 + 90, 75 - 40, 2, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 20, 75 - 40 - 20, 2, 1, anc_sunset_wall);

            Room.RoomItemManager.AddWallItemToRoom(itemId++, 110, 5, 4, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 195, 40, 4, 1, anc_sunset_wall);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, 280, 75, 4, 1, anc_sunset_wall);

            #endregion

            #region Old room
            /*
            BaseItem shelves_norja = ItemManager.AddRoomItem(baseItemId++, 13, 1, 1, 0, "shelves_norja", 1, false, false, false, new List<int>() { 0, 2 });
            BaseItem rare_dragon_0 = ItemManager.AddRoomItem(baseItemId++, 1620, 1, 1, 0, "rare_dragonlamp*0", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem hologram = ItemManager.AddRoomItem(baseItemId++, 234, 1, 1, 0, "hologram", 2, false, false, false, new List<int>() { 0 });
            BaseItem club_sofa = ItemManager.AddRoomItem(baseItemId++, 267, 2, 1, 0, "club_sofa", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem doorD = ItemManager.AddRoomItem(baseItemId++, 1505, 1, 1, 0, "doorD", 3, false, true, false, new List<int>() { 2, 4 });
            BaseItem tile_brown = ItemManager.AddRoomItem(baseItemId++, 2582, 1, 1, 0.15, "tile_brown", 1, false, true, false, new List<int>() { 0 });
            BaseItem tile_marble = ItemManager.AddRoomItem(baseItemId++, 2566, 1, 1, 0.15, "tile_marble", 1, false, true, false, new List<int>() { 0 });
            BaseItem scifidoor_10 = ItemManager.AddRoomItem(baseItemId++, 1569, 1, 1, 0, "scifidoor*10", 2, false, true, false, new List<int>() { 2, 4 });
            BaseItem scifiport_0 = ItemManager.AddRoomItem(baseItemId++, 1549, 1, 1, 0, "scifiport*0", 2, false, true, false, new List<int>() { 0, 6 });
            BaseItem rare_icecream_0 = ItemManager.AddRoomItem(baseItemId++, 1636, 1, 1, 0, "rare_icecream*0", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem rare_icecream_1 = ItemManager.AddRoomItem(baseItemId++, 1629, 1, 1, 0, "rare_icecream*1", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem hc_btlr = ItemManager.AddRoomItem(baseItemId++, 2075, 1, 1, 0, "hc_btlr", 2, false, false, false, new List<int>() { 2, 4 });
            BaseItem throne = ItemManager.AddRoomItem(baseItemId++, 230, 1, 1, 0, "throne", 0, false, true, true, new List<int>() { 2, 4 });
            BaseItem small_chair_armas = ItemManager.AddRoomItem(baseItemId++, 55, 1, 1, 0, "small_chair_armas", 0, false, true, true, new List<int>() { 2, 4, 4, 6 });
            BaseItem hcsohva = ItemManager.AddRoomItem(baseItemId++, 287, 2, 1, 0, "hcsohva", 1, false, false, true, new List<int>() { 0, 2, 4, 6 });
            BaseItem hc_tv = ItemManager.AddRoomItem(baseItemId++, 2069, 2, 1, 1.3, "hc_tv", 2, false, false, true, new List<int>() { 2, 4 });



            BaseItem hc_wall_lamp = ItemManager.AddWallItem(baseItemId++, 4003, "hc_wall_lamp", 2);
            BaseItem flag_mexico = ItemManager.AddWallItem(baseItemId++, 4250, "flag_mexico", 1);
            BaseItem flag_columbia = ItemManager.AddWallItem(baseItemId++, 4258, "flag_columbia", 1);

            z = tile_marble.Z;

            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 0, z, rare_dragon_0.Directions[1], 1, rare_dragon_0);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 3, 8, z, hologram.Directions[0], 0, hologram);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 6, z, club_sofa.Directions[1], 0, club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 6, 8, z, club_sofa.Directions[1], 0, club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 4, 12, z, hcsohva.Directions[2], 0, hcsohva);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 12, z, club_sofa.Directions[2], 0, club_sofa);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 10, z, doorD.Directions[0], 2, doorD);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 0, z, doorD.Directions[1], 1, doorD);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 1, 4, z, scifidoor_10.Directions[0], 1, scifidoor_10);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 2, 12, z, scifiport_0.Directions[0], 1, scifiport_0);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 8, 0, z, rare_icecream_1.Directions[1], 0, rare_icecream_1);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 0, z, rare_icecream_0.Directions[1], 0, rare_icecream_0);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 7, 0, z, small_chair_armas.Directions[0], 0, small_chair_armas);
            Room.RoomItemManager.AddRoomItemToRoom(itemId++, 5, 0, z, hc_btlr.Directions[1], 0, hc_btlr);

            Room.RoomItemManager.AddWallItemToRoom(itemId++, 160, 0, 4, 1, hc_wall_lamp);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -190, 59, 2, 0, flag_mexico);
            Room.RoomItemManager.AddWallItemToRoom(itemId++, -130, 30, 2, 0, flag_columbia);

            for (int i = 1; i < Room.Model.MaxX; i++)
            {
                for (int j = 0; j < Room.Model.MaxY; j++)
                {
                    Room.RoomItemManager.AddRoomItemToRoom(itemId++, i, j, 0, 0, 0, i % 2 == 0 ? tile_brown : tile_marble);
                }
            }
            */
            #endregion

            Task RoomThread = new Task(OnCycle);
            RoomThread.Start();
        }
        #endregion

        #region Methods
        internal void OnCycle()
        {
            while (true)
            {
                try
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
                    if (sleepTime > 1)
                        Thread.Sleep((int)Math.Floor(sleepTime));
                }
                catch (Exception e)
                {
                    Logging.WriteLine("RoomCycle error: " + e.ToString(), ConsoleColor.Red, LogLevel.Warning);
                }
            }
        }
        #endregion
    }
}
