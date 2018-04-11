using System;
using BattleBall.Misc;
using BattleBall.Core;

namespace BattleBall
{
    class BattleEnvironment
    {
		internal static Game Game;

        public static void Initialize()
        {
            Game = new Game();

            Logging.WriteLine("The environment has initialized successfully. Ready for connections.", ConsoleColor.Green);

            string command;
            while (true)
            {
                command = Console.ReadLine();
                string[] args = command.Split(' ');
                switch (args[0])
                {
                    case "exit":
                    case "stop":
                        {
                            Logging.WriteLine("Stopping server...", ConsoleColor.Yellow);
                            return;
                        }
                    default:
                        {
                            Logging.WriteLine("Invalid command", ConsoleColor.Red);
                            break;
                        }
                    case "move":
                        {
                            try
                            {
                                int x = int.Parse(args[1]);
                                int y = int.Parse(args[2]);
                                Game.Room.MovePlayersTo(x, y);
                            }
                            catch (FormatException)
                            {
                                Logging.WriteLine("Invalid args", ConsoleColor.Red);
                            }
                            catch (Exception)
                            {
                                
                            }
                            break;
                        }
                    case "status":
                        {
                            Logging.WriteLine("OnlineCount: " + Game.ClientManager.Clients.Count, ConsoleColor.Yellow);
                            break;
                        }
                }
            }
        }

        static void Main(string[] args)
        {
            BattleEnvironment.Initialize();
        }
    }
}
