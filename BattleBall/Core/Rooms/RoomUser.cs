using System.Collections.Generic;
using System.Drawing;
using BattleBall.Core.GameClients.Messages;
using BattleBall.Core.GameClients;
using System;

namespace BattleBall.Core.Rooms
{
    class RoomUser
    {
		internal int UserId;
        internal int X, Y, Rot;
		internal int TargetX, TargetY;
		internal bool IsMoving;
		internal bool PathRecalcNeeded;

        public Room Room { get; }
        public Team Team { get; set; }
        public LinkedList<Point> Path { get; set; }
        
        internal User User;

        public RoomUser(int userId, int x, int y, int rot, User user, Room room)
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
            this.Room = room;
        }

        public void MoveTo(int x, int y)
        {
            if (Room.ValidTile(x, y))
            {
                this.TargetX = x;
                this.TargetY = y;
                this.PathRecalcNeeded = true;
            }
        }

        internal void Chat(string Message)
        {
            ServerMessage ChatMessage = new ServerMessage(ServerOpCodes.CHAT);
            ChatMessage.AppendInt(UserId);
            ChatMessage.AppendString(User.Username);
            ChatMessage.AppendString(Message);
            Room.SendMessage(ChatMessage);
        }
    }
}
