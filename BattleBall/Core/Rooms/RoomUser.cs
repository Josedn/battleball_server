using System.Collections.Generic;
using System.Drawing;

namespace BattleBall.Core.Rooms
{
    class RoomUser
    {
        private int userId;
        private int x, y, rot;
        private int targetX, targetY;
        private bool isMoving;
        private bool pathRecalcNeeded;


        public Team Team { get; set; }
        public LinkedList<Point> Path { get; set; }
        public int UserId { get => userId; set => userId = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Rot { get => rot; set => rot = value; }
        public int TargetX { get => targetX; set => targetX = value; }
        public int TargetY { get => targetY; set => targetY = value; }
        public bool IsMoving { get => isMoving; set => isMoving = value; }
        public bool PathRecalcNeeded { get => pathRecalcNeeded; set => pathRecalcNeeded = value; }

        internal User User;

        

        public RoomUser(int userId, int x, int y, int rot, User user)
        {
            this.UserId = userId;
            this.X = x;
            this.Y = y;
            this.Rot = rot;
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
