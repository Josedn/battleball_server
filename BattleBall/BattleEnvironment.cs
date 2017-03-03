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
                switch (command)
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
                }
            }
        }

        static void Main(string[] args)
        {
            BattleEnvironment.Initialize();
        }
    }
}
