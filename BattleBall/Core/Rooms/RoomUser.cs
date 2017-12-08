using BattleBall.Communication.Outgoing.Rooms;
using BattleBall.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BattleBall.Core.Rooms
{
    class RoomUser
    {
        #region Fields
        internal int UserId;
        internal int X, Y, Rot;
        internal double Z;
		internal int TargetX, TargetY;
		internal bool IsMoving;
		internal bool PathRecalcNeeded;

        public Room Room { get; }
        public Team Team { get; set; }
        public LinkedList<Point> Path { get; set; }
        public bool NeedsUpdate { get; set; }

        internal User User;
        #endregion

        #region Constructor
        public RoomUser(int userId, int x, int y, double z, int rot, User user, Room room)
        {
            this.UserId = userId;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Rot = rot;
            this.TargetX = x;
            this.TargetY = y;
            this.IsMoving = false;
            this.PathRecalcNeeded = false;
            this.Path = new LinkedList<Point>();
            this.Team = Team.None;
            this.User = user;
            this.Room = room;
        }
        #endregion

        #region Methods
        internal void LookAt(int userId)
        {
            RoomUser otherUser = Room.GetRoomUserByUserId(userId);
            if (otherUser != null)
            {
                Rot = Room.CalculateRotation(X, Y, otherUser.X, otherUser.Y);
                NeedsUpdate = true;
            }
        }

        public void MoveTo(int x, int y)
        {
            Logging.WriteLine(User.Username + " wants to move to " + x + ", " + y, ConsoleColor.Yellow);
            if (Room.ValidTile(x, y))
            {
                this.TargetX = x;
                this.TargetY = y;
                this.PathRecalcNeeded = true;
            }
        }

        internal void Chat(string message)
        {
            Room.SendMessage(new ChatComposer(UserId, message));
        }
        #endregion
    }
}
