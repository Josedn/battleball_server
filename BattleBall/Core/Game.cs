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
        internal ConnectionManager ConnectionManager;
        internal GameClientManager ClientManager;
        internal Authenticator Authenticator;
        private const int DELTA_TIME = 500;
        #endregion

        #region Constructor
        internal Game()
        {
            this.ClientManager = new GameClientManager();
            this.ConnectionManager = new ConnectionManager(ClientManager);

            this.Authenticator = new Authenticator(this);
            this.Room = new Room(new MapModel());
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
