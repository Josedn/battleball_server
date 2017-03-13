using BattleBall.Net;
using BattleBall.Core.Rooms;
using BattleBall.Core.GameClients;

namespace BattleBall.Core
{
    internal class Game
    {
        #region Fields
        public Room Room;
        private ConnectionManager connectionManager;
        private GameClientManager clientManager;
        private MapModel mapModel;
        #endregion

        #region Return Values
        internal ConnectionManager ConnectionManager { get => connectionManager; }
        internal GameClientManager ClientManager { get => clientManager; }
        internal MapModel MapModel { get => mapModel; }
        #endregion

        #region Constructor
        internal Game()
        {
            this.clientManager = new GameClientManager();
            this.connectionManager = new ConnectionManager(clientManager);

            this.mapModel = new MapModel();
            this.Room = new Room(mapModel.Width, mapModel.Height);
        }
        #endregion

        #region Methods

        #endregion
    }
}
