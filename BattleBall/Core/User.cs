using System;
using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;
using BattleBall.Misc;

namespace BattleBall.Core
{
    class User
    {
        #region Fields
        internal int Id;
        internal string Username;
        internal string Look;
        internal GameClient Session;
        internal Room CurrentRoom;
        private bool disconnected;
        #endregion

        #region Constructor
        public User(int id, string username, string look, GameClient session)
        {
            this.Id = id;
            this.Username = username;
            this.Look = look;
            this.Session = session;
            this.disconnected = false;
        }
        #endregion

        internal RoomUser CurrentRoomUser
        {
            get
            {
                if (CurrentRoom != null)
                {
                    return CurrentRoom.GetRoomUserByUserId(Id);
                }
                return null;
            }
        }

        internal void OnDisconnect()
        {
            if (disconnected)
                return;

            Logging.WriteLine(Username + " has logged out", ConsoleColor.Red, LogLevel.Verbose);
            if (CurrentRoom != null)
            {
                CurrentRoom.RemovePlayerFromRoom(Session);
            }
        }
    }
}
