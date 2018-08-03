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
            Console.WriteLine(@"");
            Console.WriteLine(@"|         |    |          o     ");
            Console.WriteLine(@"|---.,---.|---.|---.,---. .,---.");
            Console.WriteLine(@"|   ||   ||   ||   |,---| ||   |");
            Console.WriteLine(@"`---'`---'`---'`---'`---^o``---'");
            Console.WriteLine(@"Copyright (c) 2018 - jetx");
            Console.WriteLine();
            Logging.WriteLine("The environment has initialized successfully. Ready for connections.", ConsoleColor.Green, LogLevel.Info);
            Logging.SetLogLevel(LogLevel.Verbose);

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
                            Logging.WriteLine("Stopping server...", ConsoleColor.Yellow, LogLevel.Info);
                            return;
                        }
                    case "map":
                        {
                            Core.Rooms.SqState[,] map = Game.Room.GameMap.Map;
                            for (int i = 0; i < Game.Room.GameMap.MapModel.MaxY; i++)
                            {
                                for (int j = 0; j < Game.Room.GameMap.MapModel.MaxX; j++)
                                {
                                    Console.Write((int)map[j, i] + "\t| ");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case "generatemaps":
                        {
                            Game.Room.GameMap.GenerateMaps();
                            break;
                        }
                    case "heightmap":
                        {
                            double[,] map = Game.Room.GameMap.ItemHeightMap;
                            for (int i = 0; i < Game.Room.GameMap.MapModel.MaxY; i++)
                            {
                                for (int j = 0; j < Game.Room.GameMap.MapModel.MaxX; j++)
                                {
                                    Console.Write(map[j, i] + "\t| ");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    default:
                        {
                            Logging.WriteLine("Invalid command", ConsoleColor.Red, LogLevel.Info);
                            break;
                        }
                    case "status":
                        {
                            Logging.WriteLine("OnlineCount: " + Game.ClientManager.Clients.Count, ConsoleColor.Yellow, LogLevel.Info);
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
