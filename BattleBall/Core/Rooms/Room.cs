using System.Collections.Generic;
using System.Drawing;
using BattleBall.AStar.Algorithm;

using BattleBall.Core.GameClients;
using BattleBall.Communication.Protocol;
using BattleBall.Communication.Outgoing.Rooms;
using BattleBall.Core.Items;
using BattleBall.Core.Rooms.Items;

namespace BattleBall.Core.Rooms
{
    class Room : IPathNode
    {
        #region Fields
        private int[,] PlayerMatrix;
        //private int[,] GameMatrix;

        private MapModel Model { get; }
        private Dictionary<int, RoomUser> Players;
        internal RoomItemManager RoomItemManager;
        private AStarSolver<Room> astarSolver;

        #endregion

        #region Constructor
        public Room(MapModel model)
        {
            this.Model = model;
            this.PlayerMatrix = new int[Model.Cols, Model.Rows];
            //this.GameMatrix = new int[Model.Cols, Model.Rows];
            this.Players = new Dictionary<int, RoomUser>();
            //this.RoomItems = new Dictionary<int, RoomItem>();
            this.RoomItemManager = new RoomItemManager(this);
            this.astarSolver = new AStarSolver<Room>(true, AStarHeuristicType.Between, this, Model.Cols, Model.Rows);
        }
        #endregion

        #region Methods
        

        internal void AddPlayerToRoom(GameClient Session)
        {
            RoomUser User = new RoomUser(Session.User.Id, Model.DoorX, Model.DoorY, Model.DoorZ, Model.DoorRot, Session.User, this);
            Session.User.CurrentRoom = this;
            
            SendMessage(new SerializeRoomUserComposer(User)); //Send new room user data to room
            Players.Add(User.UserId, User); //Add new room user to users list
            Session.SendMessage(new SerializeRoomUserComposer(Players.Values)); //Send all players in room to user
            Session.SendMessage(new SerializeRoomItemComposer(RoomItemManager.RoomItems.Values)); //Send all furni in room to user
        }

        internal void SendModelToPlayer(GameClient Session)
        {
            Session.SendMessage(new MapComposer(Model)); //Send map data to player
        }

        internal void RemovePlayerFromRoom(GameClient session)
        {
            if (session == null)
                return;
            RoomUser user = GetRoomUserByUserId(session.User.Id);
            if (user == null)
            {
                return;
            }
            PlayerMatrix[user.X, user.Y] = 0;

            user.User.CurrentRoom = null;
            Players.Remove(session.User.Id);

            SendMessage(new PlayerRemoveComposer(session.User.Id));
        }

        internal void MovePlayersTo(int x, int y)
        {
            foreach (RoomUser Player in Players.Values)
            {
                Player.MoveTo(x, y);
            }
        }

        internal int CalculateRotation(int X1, int Y1, int X2, int Y2)
        {
            int Rotation = 0;

            if (X1 > X2 && Y1 > Y2)
            {
                Rotation = 7;
            }
            else if (X1 < X2 && Y1 < Y2)
            {
                Rotation = 3;
            }
            else if (X1 > X2 && Y1 < Y2)
            {
                Rotation = 5;
            }
            else if (X1 < X2 && Y1 > Y2)
            {
                Rotation = 1;
            }
            else if (X1 > X2)
            {
                Rotation = 6;
            }
            else if (X1 < X2)
            {
                Rotation = 2;
            }
            else if (Y1 < Y2)
            {
                Rotation = 4;
            }
            else if (Y1 > Y2)
            {
                Rotation = 0;
            }

            return Rotation;
        }
        internal RoomUser GetRoomUserByUserId(int id)
        {
            if (Players.ContainsKey(id))
            {
                return Players[id];
            }
            return null;
        }
        internal void OnCycle()
        {
            foreach (RoomUser player in Players.Values)
            {
                if (player.PathRecalcNeeded)
                {
                    //Console.WriteLine(player.UserId + "'s needs recalc...");
                    Point start = new Point(player.X, player.Y);
                    Point end = new Point(player.TargetX, player.TargetY);
                    LinkedList<AStarSolver<Room>.PathNode> path = astarSolver.Search(end, start);

                    player.Path.Clear();

                    if (path != null)
                    {
                        path.RemoveFirst();
                        foreach (AStarSolver<Room>.PathNode node in path)
                        {
                            player.Path.AddLast(new Point(node.X, node.Y));
                        }
                    }
                    else
                    {
                        player.PathRecalcNeeded = false;
                        player.IsMoving = false;
                    }
                    player.PathRecalcNeeded = false;
                    player.IsMoving = true;
                }

                if (player.NeedsUpdate)
                {
                    SendMessage(new SerializeRoomUserComposer(player)); //Send new room user data to room
                    player.NeedsUpdate = false;
                }

                if (player.IsMoving)
                {
                    //Console.WriteLine(player.UserId + "'s is moving...");
                    if (player.Path.Count > 1)
                    {
                        //TODO: Check if player is candidate tile is valid

                        OnPlayerWalksOffTile(player, player.X, player.Y);

                        player.Rot = CalculateRotation(player.X, player.Y, player.Path.First.Value.X, player.Path.First.Value.Y);

                        player.X = player.Path.First.Value.X;
                        player.Y = player.Path.First.Value.Y;

                        PlayerMatrix[player.X, player.Y] = player.UserId;
                        player.Path.RemoveFirst();
                        if (player.TargetX == player.X && player.TargetY == player.Y)
                        {
                            player.IsMoving = false;
                        }

                        OnPlayerWalksOnTile(player, player.X, player.Y);

                        SendMessage(new PlayerMovementComposer(player.UserId, player.X, player.Y, player.Rot));
                    }
                    else
                    {
                        player.IsMoving = false;
                    }
                }
            }
        }

        internal bool ValidTile(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Model.Cols && y < Model.Rows && Model.Map[x, y] != 0;
        }

        public bool IsBlocked(int x, int y, bool lastTile)
        {
            return !ValidTile(x, y) || PlayerMatrix[x, y] != 0;
        }

        internal void OnPlayerWalksOnTile(RoomUser player, int x, int y)
        {
            //GameMatrix[x, y] = (int)player.Team;
            PlayerMatrix[x, y] = player.UserId;
        }

        internal void SendMessage(ServerMessage Message)
        {
            List<RoomUser> playersToSend;
            lock (Players.Values)
            {
                playersToSend = new List<RoomUser>(Players.Values);
            }

            foreach (RoomUser user in playersToSend)
            {
                if (user != null)
                {
                    user.User.Session.SendMessage(Message);
                }
            }
        }

        internal void OnPlayerWalksOffTile(RoomUser player, int x, int y)
        {
            PlayerMatrix[player.X, player.Y] = 0;
        }
        #endregion
    }
}
