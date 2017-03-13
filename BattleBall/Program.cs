using System;
using BattleBall.Misc;
using BattleBall.Core.Rooms;

namespace BattleBall
{
    class Program
    {
        static char GetPrintableElement(int e)
        {
            if (e == 0)
                return ' ';
            if (e == 1)
                return '1';
            if (e == 2)
                return '2';
            return '-';
        }
        static void ShowMap(Room map)
        {
            for (int j = 0; j < map.MaxY; j++)
            {
                for (int i = 0; i < map.MaxX; i++)
                {
                    ConsoleColor color = ConsoleColor.White;
                    if (map.GameMatrix[i, j] == 1)
                    {
                        color = ConsoleColor.Red;
                    }
                    else if (map.GameMatrix[i, j] == 2)
                    {
                        color = ConsoleColor.Blue;
                    }
                    Logging.Write("[" + GetPrintableElement(map.PlayerMatrix[i, j]) + "]", color);
                }
                Logging.WriteLine("", ConsoleColor.Gray);
            }
        }

        static void StartGame()
        {
            RoomUser player1 = new RoomUser(1, 1, 2);
            RoomUser player2 = new RoomUser(2, 1, 1);

            player1.Team = Team.Blue;
            player2.Team = Team.Red;

            player1.MoveTo(5, 4);
            player2.MoveTo(0, 0);

            Room map = new Room(6, 6);
            map.AddPlayer(player1);
            map.AddPlayer(player2);

            while (true)
            {
                ShowMap(map);
                Console.WriteLine("OnCycle?");
                Console.ReadLine();
                map.OnCycle();
            }
        }

        static void Main2(string[] args)
        {
            StartGame();
            /*
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
            });

            Console.ReadLine();*/
        }
    }
}
