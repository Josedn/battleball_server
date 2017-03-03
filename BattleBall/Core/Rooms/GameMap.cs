using System;
using System.Collections.Generic;
using BattleBall.AStar.Algorithm;
using System.Drawing;

namespace BattleBall.Core.Rooms
{
    class GameMap : IPathNode
    {
        #region Fields
        private int[,] playerMatrix;
        private int[,] gameMatrix;
        private List<Player> players;
        private readonly int maxX, maxY;
        private AStarSolver<GameMap> astarSolver;

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

        internal void AddPlayer(Player player)
        {
            if (!players.Contains(player))
            {
                this.players.Add(player);
            }
            this.PlayerMatrix[player.X, player.Y] = player.Id;
        }
        #endregion

        #region Constructor
        public GameMap(int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.playerMatrix = new int[maxX, maxY];
            this.gameMatrix = new int[maxX, maxY];
            this.players = new List<Player>();
            this.astarSolver = new AStarSolver<GameMap>(false, AStarHeuristicType.ExperimentalSearch, this, maxX, maxY);
        }
        #endregion

        #region Methods
        internal void OnCycle()
        {
            Console.WriteLine("OnCyle()");
            foreach (Player player in players)
            {
                if (player.PathRecalcNeeded)
                {
                    Console.WriteLine(player.Id + "'s needs recalc...");
                    Point start = new Point(player.X, player.Y);
                    Point end = new Point(player.TargetX, player.TargetY);
                    LinkedList<AStarSolver<GameMap>.PathNode> path = astarSolver.Search(end, start);

                    player.Path.Clear();

                    if (path != null)
                    {
                        path.RemoveFirst();
                        foreach (AStarSolver<GameMap>.PathNode node in path)
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
                    Console.WriteLine(player.Id + "'s is moving...");
                    if (player.Path.Count > 1)
                    {
                        PlayerMatrix[player.X, player.Y] = 0;
                        //TODO: Check if player is candidate tile is valid

                        OnPlayerWalksOffTile(player, player.X, player.Y);

                        player.X = player.Path.First.Value.X;
                        player.Y = player.Path.First.Value.Y;

                        PlayerMatrix[player.X, player.Y] = player.Id;
                        player.Path.RemoveFirst();
                        if (player.TargetX == player.X && player.TargetY == player.Y)
                        {
                            player.IsMoving = false;
                        }

                        OnPlayerWalksOnTile(player, player.X, player.Y);
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

        internal void OnPlayerWalksOnTile(Player player, int x, int y)
        {
            gameMatrix[x, y] = (int)player.Team;
        }

        internal void OnPlayerWalksOffTile(Player player, int x, int y)
        {

        }
        #endregion
    }
}
