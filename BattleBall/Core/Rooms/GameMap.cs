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
    class GameMap
    {
        private Room Room;
        private MapModel MapModel;
        //private bool DiagonalEnabled;
        private byte[,] Map;
        private double[,] ItemHeightMap;
        private Dictionary<Point, List<RoomItem>> CoordinatedItems;
        private Dictionary<Point, List<RoomUser>>  CoordinatedUsers;

        public GameMap(Room room, MapModel mapModel)
        {
            Room = room;
            MapModel = mapModel;
            //DiagonalEnabled = true;
            CoordinatedItems = new Dictionary<Point, List<RoomItem>>();
            CoordinatedUsers = new Dictionary<Point, List<RoomUser>>();
            Map = new byte[MapModel.Cols, MapModel.Rows];
            ItemHeightMap = new double[MapModel.Cols, MapModel.Rows];
        }

        internal void AddUserToMap(RoomUser user, Point coord)
        {
            if (!CoordinatedUsers.ContainsKey(coord))
            {
                CoordinatedUsers[coord] = new List<RoomUser>();
            }
            CoordinatedUsers[coord].Add(user);
        }

        internal void AddItemToMap(RoomItem item)
        {
            foreach (Point coord in item.Coords)
            {
                AddCoordinatedItem(item, coord);
            }
        }

        internal void AddCoordinatedItem(RoomItem item, Point coord)
        {
            List<RoomItem> Items = new List<RoomItem>(); //mCoordinatedItems[CoordForItem];
            if (!CoordinatedItems.ContainsKey(coord))
            {
                CoordinatedItems[coord] = new List<RoomItem>();
            }
            if (!CoordinatedItems[coord].Contains(item))
            {
                CoordinatedItems[coord].Add(item);
            }
        }

        internal static List<Point> GetAffectedTiles(int Length, int Width, int PosX, int PosY, int Rotation)
        {
            List<Point> PointList = new List<Point>();

            if (Length > 1)
            {
                if (Rotation == 0 || Rotation == 4)
                {
                    for (int i = 1; i < Length; i++)
                    {
                        PointList.Add(new Point(PosX, PosY + i));

                        for (int j = 1; j < Width; j++)
                        {
                            PointList.Add(new Point(PosX + j, PosY + i));
                        }
                    }
                }
                else if (Rotation == 2 || Rotation == 6)
                {
                    for (int i = 1; i < Length; i++)
                    {
                        PointList.Add(new Point(PosX + i, PosY));

                        for (int j = 1; j < Width; j++)
                        {
                            PointList.Add(new Point(PosX + i, PosY + j));
                        }
                    }
                }
            }

            if (Width > 1)
            {
                if (Rotation == 0 || Rotation == 4)
                {
                    for (int i = 1; i < Width; i++)
                    {
                        PointList.Add(new Point(PosX + i, PosY));

                        for (int j = 1; j < Length; j++)
                        {
                            PointList.Add(new Point(PosX + i, PosY + j));
                        }
                    }
                }
                else if (Rotation == 2 || Rotation == 6)
                {
                    for (int i = 1; i < Width; i++)
                    {
                        PointList.Add(new Point(PosX, PosY + i));

                        for (int j = 1; j < Length; j++)
                        {
                            PointList.Add(new Point(PosX + j, PosY + i));
                        }
                    }
                }
            }

            return PointList;
        }

    }
}
