using System;
using System.Collections.Generic;
using BattleBall.AStar.Algorithm;
using System.Drawing;
using BattleBall.Core.GameClients;
using BattleBall.Core.GameClients.Messages;

namespace BattleBall.Core.Rooms
{
    class Room : IPathNode
    {
        #region Fields
        private int[,] playerMatrix;
        private int[,] gameMatrix;
        //private List<RoomUser> players;
        private Dictionary<int, RoomUser> players;
        private readonly int maxX, maxY;
        private AStarSolver<Room> astarSolver;

        public int[,] GameMatrix
        {
            get
            {
                return gameMatrix;
            }
        }
        public int[,] PlayerMatrix
        {
            get
            {
                return playerMatrix;
            }
        }

        public int MaxX
        {
            get
            {
                return maxX;
            }
        }

        public int MaxY
        {
            get
            {
                return maxY;
            }
        }

        internal Dictionary<int, RoomUser> Players { get => players; set => players = value; }

        internal void AddPlayerToRoom(GameClient Session)
        {
            int x = 3;
            int y = 7;
            RoomUser User = new RoomUser(Session.User.Id, x, y, Session.User);
            Session.User.CurrentRoom = this;
            Players.Add(User.UserId, User);
            PlayerMatrix[User.X, User.Y] = User.UserId;

            SerializeRoomUsers();
        }

        internal void RemoveUserFromRoom(GameClient session)
        {
            if (session == null)
                return;
            RoomUser user = GetRoomUserByUserId(session.User.Id);
            if (user == null)
            {
                return;
            }
            playerMatrix[user.X, user.Y] = 0;

            this.players.Remove(session.User.Id);

            SerializeRoomUsers();
        }

        internal void SerializeRoomUsers()
        {
            ServerMessage response = new ServerMessage(ServerOpCodes.PLAYERS_DATA);
            response.AppendInt(BattleEnvironment.Game.Room.Players.Count);

            foreach (RoomUser Player in BattleEnvironment.Game.Room.Players.Values)
            {
                response.AppendInt(Player.UserId);
                response.AppendInt(Player.X);
                response.AppendInt(Player.Y);
                response.AppendString(Player.User.Look);
            }

            SendMessage(response);
        }

        internal void MovePlayersTo(int x, int y)
        {
            foreach (RoomUser Player in Players.Values)
            {
                Player.MoveTo(x, y);
            }
        }
        #endregion

        #region Constructor
        public Room(int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.playerMatrix = new int[maxX, maxY];
            this.gameMatrix = new int[maxX, maxY];
            this.players = new Dictionary<int, RoomUser>();
            this.astarSolver = new AStarSolver<Room>(false, AStarHeuristicType.ExperimentalSearch, this, maxX, maxY);
        }
        #endregion

        #region Methods

        internal RoomUser GetRoomUserByUserId(int id)
        {
            if (players.ContainsKey(id))
            {
                return players[id];
            }
            return null;
        }
        internal void OnCycle()
        {
            foreach (RoomUser player in players.Values)
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

                if (player.IsMoving)
                {
                    //Console.WriteLine(player.UserId + "'s is moving...");
                    if (player.Path.Count > 1)
                    {
                        PlayerMatrix[player.X, player.Y] = 0;
                        //TODO: Check if player is candidate tile is valid

                        OnPlayerWalksOffTile(player, player.X, player.Y);

                        player.X = player.Path.First.Value.X;
                        player.Y = player.Path.First.Value.Y;

                        PlayerMatrix[player.X, player.Y] = player.UserId;
                        player.Path.RemoveFirst();
                        if (player.TargetX == player.X && player.TargetY == player.Y)
                        {
                            player.IsMoving = false;
                        }

                        OnPlayerWalksOnTile(player, player.X, player.Y);

                        ServerMessage movementMessage = new ServerMessage(ServerOpCodes.PLAYER_MOVEMENT);
                        movementMessage.AppendInt(player.UserId);
                        movementMessage.AppendInt(player.X);
                        movementMessage.AppendInt(player.Y);
                        SendMessage(movementMessage);
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
            return x >= 0 && y >= 0 && x < MaxX && y < MaxY;
        }

        public bool IsBlocked(int x, int y, bool lastTile)
        {
            return !ValidTile(x, y) || PlayerMatrix[x, y] != 0;
        }

        internal void OnPlayerWalksOnTile(RoomUser player, int x, int y)
        {
            gameMatrix[x, y] = (int)player.Team;
        }

        internal void SendMessage(ServerMessage Message)
        {
            foreach (RoomUser user in players.Values)
            {
                user.User.Session.SendMessage(Message);
            }
        }

        internal void OnPlayerWalksOffTile(RoomUser player, int x, int y)
        {

        }
        #endregion
    }
}
