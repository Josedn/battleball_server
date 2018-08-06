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
            RoomUser targetRoomUser = null;

            try
            {
                switch (args[0].ToLower())
                {
                    case "sit":
                        {
                            if (!currentUser.Statusses.ContainsKey("sit"))
                            {
                                if (currentUser.Rot % 2 == 1)
                                {
                                    currentUser.Rot--;
                                }
                                currentUser.AddStatus("sit", "0.55");
                                currentUser.NeedsUpdate = true;
                            }
                            else
                            {
                                currentUser.Room.UpdateUserStatus(currentUser);
                            }
                        }
                        return true;
                    case "updateuser":
                        {
                            currentUser.Room.UpdateUserStatus(currentUser);
                        }
                        return true;
                    case "push":
                        {
                            if (args.Length > 1)
                            {
                                targetRoomUser = currentUser.Room.GetRoomUserByName(args[1]);
                            }
                            else
                            {
                                if (currentUser.Rot == 4)
                                    targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X, currentUser.Y + 1).FirstOrDefault();
                                if (currentUser.Rot == 0)
                                    targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X, currentUser.Y - 1).FirstOrDefault();
                                if (currentUser.Rot == 6)
                                    targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X - 1, currentUser.Y).FirstOrDefault();
                                if (currentUser.Rot == 2)
                                    targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X + 1, currentUser.Y).FirstOrDefault();
                            }
                            if (targetRoomUser != null)
                            {
                                if ((targetRoomUser.X == currentUser.X - 1) || (targetRoomUser.X == currentUser.X + 1) || (targetRoomUser.Y == currentUser.Y - 1) || (targetRoomUser.Y == currentUser.Y + 1))
                                {
                                    if (currentUser.Rot == 4)
                                    { targetRoomUser.MoveTo(targetRoomUser.X, targetRoomUser.Y + 1); }

                                    if (currentUser.Rot == 0)
                                    { targetRoomUser.MoveTo(targetRoomUser.X, targetRoomUser.Y - 1); }

                                    if (currentUser.Rot == 6)
                                    { targetRoomUser.MoveTo(targetRoomUser.X - 1, targetRoomUser.Y); }

                                    if (currentUser.Rot == 2)
                                    { targetRoomUser.MoveTo(targetRoomUser.X + 1, targetRoomUser.Y); }

                                    if (currentUser.Rot == 3)
                                    {
                                        targetRoomUser.MoveTo(targetRoomUser.X + 1, targetRoomUser.Y + 1);
                                    }

                                    if (currentUser.Rot == 1)
                                    {
                                        targetRoomUser.MoveTo(targetRoomUser.X + 1, targetRoomUser.Y - 1);
                                    }

                                    if (currentUser.Rot == 7)
                                    {
                                        targetRoomUser.MoveTo(targetRoomUser.X - 1, targetRoomUser.Y - 1);
                                    }

                                    if (currentUser.Rot == 5)
                                    {
                                        targetRoomUser.MoveTo(targetRoomUser.X - 1, targetRoomUser.Y + 1);
                                    }

                                    currentUser.Chat("*pushes " + targetRoomUser.User.Username + "*");
                                }
                            }
                        }
                        return true;

                    case "pull":
                        {
                            if (currentUser.Rot == 4)
                                targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X, currentUser.Y + 2).FirstOrDefault();
                            if (currentUser.Rot == 0)
                                targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X, currentUser.Y - 2).FirstOrDefault();
                            if (currentUser.Rot == 6)
                                targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X - 2, currentUser.Y).FirstOrDefault();
                            if (currentUser.Rot == 2)
                                targetRoomUser = currentUser.Room.GameMap.GetRoomUsersForSquare(currentUser.X + 2, currentUser.Y).FirstOrDefault();

                            if (targetRoomUser != null)
                            {
                                if (currentUser.Rot == 0)
                                {
                                    targetRoomUser.MoveTo(targetRoomUser.X, targetRoomUser.Y + 1);
                                }
                                if (currentUser.Rot == 4)
                                {
                                    targetRoomUser.MoveTo(targetRoomUser.X, targetRoomUser.Y - 1);
                                }
                                if (currentUser.Rot == 2)
                                {
                                    targetRoomUser.MoveTo(targetRoomUser.X - 1, targetRoomUser.Y);
                                }
                                if (currentUser.Rot == 6)
                                {
                                    targetRoomUser.MoveTo(targetRoomUser.X + 1, targetRoomUser.Y);
                                }

                                currentUser.Chat("*pulls " + targetRoomUser.User.Username + "*");
                            }
                        }
                        return true;

                    case "maps":
                        currentUser.Room.GameMap.GenerateMaps();
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
