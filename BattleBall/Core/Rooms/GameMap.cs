using BattleBall.Core.Rooms.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms
{
    enum SqState : byte
    {
        Closed = 0,
        Walkable = 1,
        WalkableLast = 3,
        Idk = 4
    }
    class GameMap
    {
        private Room Room;
        public MapModel MapModel;
        public bool DiagonalEnabled { get; set; }
        private Dictionary<Point, List<RoomItem>> CoordinatedItems;
        private Dictionary<Point, List<RoomUser>>  CoordinatedUsers;

        public SqState[,] Map { get; private set; }

        public double[,] ItemHeightMap { get; private set; }

        public GameMap(Room room, MapModel mapModel)
        {
            Room = room;
            MapModel = mapModel;
            DiagonalEnabled = true;
            CoordinatedItems = new Dictionary<Point, List<RoomItem>>();
            CoordinatedUsers = new Dictionary<Point, List<RoomUser>>();
            Map = new SqState[MapModel.MaxX, MapModel.MaxY];
            ItemHeightMap = new double[MapModel.MaxX, MapModel.MaxY];
        }

        internal void AddUserToMap(RoomUser user, Point coord)
        {
            if (!CoordinatedUsers.ContainsKey(coord))
            {
                CoordinatedUsers[coord] = new List<RoomUser>();
            }
            if (!CoordinatedUsers[coord].Contains(user))
            {
                CoordinatedUsers[coord].Add(user);
            }
        }

        internal void AddItemToMap(RoomItem item)
        {

            //Check interactiontype

            foreach (Point coord in item.Coords)
            {
                AddCoordinatedItem(item, coord);
                ConstructMapForItem(item, coord);
            }


        }

        private void ConstructMapForItem(RoomItem item, Point coord)
        {
            if (ItemHeightMap[coord.X, coord.Y] <= item.TotalHeight) // If it is top of stack
            {
                ItemHeightMap[coord.X, coord.Y] = item.TotalHeight;
                if (item.BaseItem.Walkable)
                {
                    Map[coord.X, coord.Y] = SqState.Walkable;
                }
                else if (item.BaseItem.IsSeat)
                {
                    Map[coord.X, coord.Y] = SqState.WalkableLast;
                }
                else
                {
                    Map[coord.X, coord.Y] = SqState.Closed;
                }
            }
        }

        private void AddCoordinatedItem(RoomItem item, Point coord)
        {
            if (!CoordinatedItems.ContainsKey(coord))
            {
                CoordinatedItems[coord] = new List<RoomItem>();
            }
            if (!CoordinatedItems[coord].Contains(item))
            {
                CoordinatedItems[coord].Add(item);
            }
        }

        internal void RemoveItemFromMap(RoomItem item)
        {
            Dictionary<Point, List<RoomItem>> otherItems = new Dictionary<Point, List<RoomItem>>();
            foreach (Point coord in item.Coords)
            {
                RemoveCoordinatedItem(item, coord);
                if (!otherItems.ContainsKey(coord))
                {
                    otherItems.Add(coord, CoordinatedItems[coord]);
                }
                SetDefaultValue(coord.X, coord.Y);
            }

            foreach (Point coord in otherItems.Keys)
            {
                List<RoomItem> itemsToAdd = otherItems[coord];
                if (itemsToAdd != null)
                {
                    ConstructMapForItem(item, coord);
                }
            }
        }

        private void SetDefaultValue(int x, int y)
        {
            Map[x, y] = SqState.Closed;
            ItemHeightMap[x, y] = 0;
            if (x == MapModel.DoorX && y == MapModel.DoorY)
            {
                Map[x, y] = SqState.WalkableLast;
            }
        }

        private void RemoveCoordinatedItem(RoomItem item, Point coord)
        {
            if (CoordinatedItems.ContainsKey(coord))
            {
                CoordinatedItems[coord].Remove(item);
            }
        }

        internal void UpdateUserMovement(Point oldCoord, Point newCoord, RoomUser user)
        {
            RemoveUserFromMap(user, oldCoord);
            AddUserToMap(user, newCoord);
        }

        public void RemoveUserFromMap(RoomUser user, Point coord)
        {
            if (CoordinatedUsers.ContainsKey(coord))
                CoordinatedUsers[coord].Remove(user);
        }

        internal bool CanWalk(int x, int y)
        {
            return !SquareGotUsers(x, y);
        }

        internal bool SquareGotUsers(int x, int y)
        {
            return GetRoomUsersForSquare(x, y).Count > 0;
        }

        internal List<RoomUser> GetRoomUsersForSquare(int x, int y)
        {
            Point coord = new Point(x, y);
            if (CoordinatedUsers.ContainsKey(coord))
            {
                return CoordinatedUsers[coord];
            }
            return new List<RoomUser>();
        }

        internal List<RoomItem> GetCoordinatedHeighestItems(int x, int y)
        {
            Point coord = new Point(x, y);

            if (!CoordinatedItems.ContainsKey(coord))
                return new List<RoomItem>();

            List<RoomItem> items = CoordinatedItems[coord];
                
            if (items.Count == 1)
                return items.ToList();
            List<RoomItem> returnItems = new List<RoomItem>();
            double heighest = -1;
            foreach (RoomItem i in items)
            {
                if (i.TotalHeight > heighest)
                {
                    heighest = i.Z;
                    returnItems.Clear();
                    returnItems.Add(i);
                }
                else if (i.TotalHeight == heighest)
                {
                    returnItems.Add(i);
                }
            }

            return returnItems;
        }

        internal List<RoomItem> GetRoomItemsForSquare(int x, int y)
        {
            Point coord = new Point(x, y);
            if (CoordinatedUsers.ContainsKey(coord))
            {
                return CoordinatedItems[coord];
            }
            return new List<RoomItem>();
        }

        internal static int CalculateRotation(int X1, int Y1, int X2, int Y2)
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

        internal void GenerateMaps()
        {
            Map = new SqState[MapModel.MaxX, MapModel.MaxY];
            ItemHeightMap = new double[MapModel.MaxX, MapModel.MaxY];

            for (int i = 0; i < MapModel.MaxX; i++)
            {
                for (int j = 0; j < MapModel.MaxY; j++)
                {
                    Map[i, j] = SqState.Walkable;
                }
            }

            List<RoomItem> roomItems;
            lock (Room.RoomItemManager.RoomItems)
            {
                roomItems = Room.RoomItemManager.RoomItems.Values.ToList();
            }

            foreach (RoomItem item in roomItems)
            {
                AddItemToMap(item);
            }

            List<RoomUser> roomUsers;
            lock (Room.Players)
            {
                roomUsers = Room.Players.Values.ToList();
            }

            foreach (RoomUser user in roomUsers)
            {
                user.CurrentSqState = Map[user.X, user.Y];
                Map[user.X, user.Y] = SqState.Closed;
            }

            Map[MapModel.DoorX, MapModel.DoorY] = SqState.WalkableLast;
        }
        
        internal static List<Point> GetAffectedTiles(int X, int Y, int PosX, int PosY, int Rotation)
        {
            List<Point> PointList = new List<Point>();

            if (Y > 1)
            {
                if (Rotation == 0 || Rotation == 4)
                {
                    for (int i = 1; i < Y; i++)
                    {
                        PointList.Add(new Point(PosX, PosY + i));

                        for (int j = 1; j < X; j++)
                        {
                            PointList.Add(new Point(PosX + j, PosY + i));
                        }
                    }
                }
                else if (Rotation == 2 || Rotation == 6)
                {
                    for (int i = 1; i < Y; i++)
                    {
                        PointList.Add(new Point(PosX + i, PosY));

                        for (int j = 1; j < X; j++)
                        {
                            PointList.Add(new Point(PosX + i, PosY + j));
                        }
                    }
                }
            }

            if (X > 1)
            {
                if (Rotation == 0 || Rotation == 4)
                {
                    for (int i = 1; i < X; i++)
                    {
                        PointList.Add(new Point(PosX + i, PosY));

                        for (int j = 1; j < Y; j++)
                        {
                            PointList.Add(new Point(PosX + i, PosY + j));
                        }
                    }
                }
                else if (Rotation == 2 || Rotation == 6)
                {
                    for (int i = 1; i < X; i++)
                    {
                        PointList.Add(new Point(PosX, PosY + i));

                        for (int j = 1; j < Y; j++)
                        {
                            PointList.Add(new Point(PosX + j, PosY + i));
                        }
                    }
                }
            }

            return PointList;
        }

        internal double SqAbsoluteHeight(int x, int y)
        {
            Point point = new Point(x, y);
            if (CoordinatedItems.ContainsKey(point))
            {
                List<RoomItem> items = CoordinatedItems[point];
                return SqAbsoluteHeight(items);
            }

            return 0;
        }

        internal double SqAbsoluteHeight(List<RoomItem> itemsOnSquare)
        {
            try
            {
                //List<RoomItem> ItemsOnSquare = GetFurniObjects(X, Y);
                double HighestStack = 0;

                bool deduct = false;
                double deductable = 0.0;


                foreach (RoomItem Item in itemsOnSquare)
                {
                    if (Item.TotalHeight > HighestStack)
                    {
                        if (Item.BaseItem.IsSeat)
                        {
                            deduct = true;
                            deductable = Item.BaseItem.Z;
                        }
                        else
                        {
                            deduct = false;
                        }

                        HighestStack = Item.TotalHeight;
                    }
                }

                double floorHeight = 0;
                double stackHeight = HighestStack - 0;

                if (deduct)
                    stackHeight -= deductable;

                if (stackHeight < 0)
                    stackHeight = 0;

                return (floorHeight + stackHeight);
            }
            catch (Exception e)
            {
                Misc.Logging.WriteLine("Room.SqAbsoluteHeight Exception: " + e.ToString(), ConsoleColor.Red, Misc.LogLevel.Warning);
                return 0;
            }
        }
    }
}
