using BattleBall.Communication.Outgoing.Rooms;
using BattleBall.Core.GameClients;
using BattleBall.Misc;
using System;

namespace BattleBall.Core
{
    class Authenticator
    {
        public static int NextId = 0;
        public Game Game;

        public Authenticator(Game game)
        {
            Game = game;
        }

        internal void TryLogin(GameClient session, string username, string look)
        {
            if (session.User == null)
            {
                session.User = new User(NextId++, username, look, session);
                Logging.WriteLine(username + " (" + session.User.Id + ") has logged in!", ConsoleColor.Green);

                session.SendMessage(new LoginOkComposer());
            }
            else
            {
                Logging.WriteLine("Client already logged!", ConsoleColor.Red);
                session.Stop();
            }
        }
    }
}
