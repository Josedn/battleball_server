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
        internal int SetX, SetY;
        internal double SetZ;
        internal bool IsWalking;
        internal Dictionary<string, string> Statusses;
        internal SqState CurrentSqState;

        public Room Room { get; }
        public bool NeedsUpdate { get; set; }
        public bool SetStep { get; set; }

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
            this.SetX = x;
            this.SetY = y;
            this.SetZ = z;
            this.IsWalking = false;
            this.User = user;
            this.Room = room;
            this.Statusses = new Dictionary<string, string>();
            this.CurrentSqState = SqState.WalkableLast;
            this.SetStep = false;
        }
        #endregion

        #region Methods
        internal void AddStatus(string key, string value)
        {
            Statusses[key] = value;
        }

        internal void RemoveStatus(string key)
        {
            if (Statusses.ContainsKey(key))
            {
                Statusses.Remove(key);
            }
        }

        internal void FurniInteract(int itemId)
        {
            if (Room != null)
            {
                Room.RoomItemManager.FurniInteract(this, itemId);
            }
        }

        internal void LookAt(int userId)
        {
            RoomUser otherUser = Room.GetRoomUserByUserId(userId);
            if (otherUser != null)
            {
                Rot = GameMap.CalculateRotation(X, Y, otherUser.X, otherUser.Y);
                NeedsUpdate = true;
            }
        }

        internal void Wave()
        {
            Room.SendMessage(new WaveComposer(UserId));
        }

        internal void MoveTo(int x, int y)
        {
            Logging.WriteLine(User.Username + " wants to move to " + x + ", " + y, ConsoleColor.Yellow, LogLevel.Debug);
            if (Room.ValidTile(x, y))
            {
                this.TargetX = x;
                this.TargetY = y;
                this.IsWalking = true;
            }
        }

        internal void Chat(string message)
        {
            if (message.ToLower().Contains("o/"))
                Wave();
            Room.SendMessage(new ChatComposer(UserId, message));
        }
        #endregion
    }
}
