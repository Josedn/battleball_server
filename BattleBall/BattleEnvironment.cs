using System;
using BattleBall.Misc;
using BattleBall.Core;

namespace BattleBall
{
    class BattleEnvironment
    {
        private static Game game;

        internal static Game Game { get => game; }

        public static void Initialize()
        {
            game = new Game();

            Logging.WriteLine("The environment has initialized successfully. Ready for connections.", ConsoleColor.Green);

            string command;
            while (true)
            {
                command = Console.ReadLine();
                string[] args = command.Split(' ');
                switch (args[0])
                {
                    case "exit":
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
                            int x = int.Parse(args[1]);
                            int y = int.Parse(args[2]); 
                            game.Room.MovePlayersTo(x, y);

                            break;
                        }
                    case "cycle":
                        {
                            game.OnCycle();
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
