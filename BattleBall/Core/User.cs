using BattleBall.Core.GameClients;
using BattleBall.Core.Rooms;

namespace BattleBall.Core
{
    class User
    {
        #region Fields
        private int id;
        private string username;
        private string look;
        private GameClient session;
        private Room currentRoom;
        #endregion

        #region Return values
        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Look { get => look; set => look = value; }
        internal GameClient Session { get => session; set => session = value; }
        internal Room CurrentRoom { get => currentRoom; set => currentRoom = value; }
        #endregion

        #region Constructor
        public User(int id, string username, string look, GameClient session)
        {
            this.id = id;
            this.username = username;
            this.look = look;
            this.session = session;
        }
        #endregion 
    }
}
