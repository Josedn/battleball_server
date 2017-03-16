using System;
using System.Threading;
using System.Threading.Tasks;
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
        private const int DELTA_TIME = 500;
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
            Task RoomThread = new Task(OnCycle);
            RoomThread.Start();
        }
        #endregion

        #region Methods
        internal void OnCycle()
        {
            while (true)
            {
                DateTime startTaskTime;
                TimeSpan spentTime;
                startTaskTime = DateTime.Now;

                Room.OnCycle();

                spentTime = DateTime.Now - startTaskTime;

                double sleepTime = DELTA_TIME - spentTime.TotalMilliseconds;

                if (sleepTime < 0)
                {
                    sleepTime = 0;
                }

                if (sleepTime > DELTA_TIME)
                {
                    sleepTime = DELTA_TIME;
                }

                Thread.Sleep((int)Math.Floor(sleepTime));
            }
        }
        #endregion
    }
}
