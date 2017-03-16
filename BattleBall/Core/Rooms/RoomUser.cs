using System.Collections.Generic;
using System.Drawing;

namespace BattleBall.Core.Rooms
{
    class RoomUser
    {
        private int userId;
        private int x, y;
        private int targetX, targetY;
        private bool isMoving;
        private bool pathRecalcNeeded;
        public Team Team { get; set; }
        public LinkedList<Point> Path { get; set; }

        internal User User;

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int TargetX
        {
            get
            {
                return targetX;
            }

            set
            {
                targetX = value;
            }
        }

        public int TargetY
        {
            get
            {
                return targetY;
            }

            set
            {
                targetY = value;
            }
        }

        public bool IsMoving
        {
            get
            {
                return isMoving;
            }

            set
            {
                isMoving = value;
            }
        }

        public bool PathRecalcNeeded
        {
            get
            {
                return pathRecalcNeeded;
            }

            set
            {
                pathRecalcNeeded = value;
            }
        }


        public RoomUser(int userId, int x, int y, User user)
        {
            this.UserId = userId;
            this.X = x;
            this.Y = y;
            this.TargetX = x;
            this.TargetY = y;
            this.IsMoving = false;
            this.PathRecalcNeeded = false;
            this.Path = new LinkedList<Point>();
            this.Team = Team.None;
            this.User = user;
        }

        public void MoveTo(int x, int y)
        {
            this.TargetX = x;
            this.TargetY = y;
            this.PathRecalcNeeded = true;
        }
    }
}
