using BattleBall.Core.GameClients;
using BattleBall.Core.Items;
using BattleBall.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms
{
    class SimpleChatCommandHandler
    {
        public static bool Parse(RoomUser currentUser, string input)
        {
            string[] args = input.Split(' ');

            if (currentUser == null || currentUser.User == null || currentUser.User.Session == null)
                return false;

            //string TargetUser = null;
            //GameClient TargetClient = null;
            //Room TargetRoom = null;
            //RoomUser targetRoomUser = null;

            try
            {
                switch (args[0].ToLower())
                {
                    case "push":
                    case "pull":
                        {
                            currentUser.Chat("Comming soon...");
                        }
                        return true;
                    case "dump":
                        {
                            currentUser.Room.RoomItemManager.RemoveAllFurniture();
                        }
                        return true;
                    case "spawn":
                        {
                            string itemName = args[1];
                            BaseItem item = BattleEnvironment.Game.ItemManager.FindItem(itemName);
                            if (item != null)
                            {
                                int rot = currentUser.Rot;
                                if (!item.Directions.Contains(rot))
                                    rot = item.Directions[0];
                                currentUser.Room.RoomItemManager.AddRoomItemToRoom(Game.itemId++, currentUser.X, currentUser.Y, currentUser.Z, rot, 0, item);
                            }
                        }
                        return true;
                    case "coords":
                        {
                            currentUser.Chat("My coords: " + currentUser.X + ", " + currentUser.Y + ", " + TextHandling.GetString(currentUser.Z) + ", Rot: " + currentUser.Rot);
                        }
                        return true;
                }
            }
            catch (Exception e)
            {
                Logging.WriteLine("Exception handling command " + input, ConsoleColor.Red, LogLevel.Warning);
                Logging.WriteLine(e, ConsoleColor.DarkRed, LogLevel.Verbose);
            }

            return false;
        }
    }
}
