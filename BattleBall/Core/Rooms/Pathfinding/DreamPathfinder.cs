using System;

namespace BattleBall.Core.Rooms.Pathfinding
{
    static class DreamPathfinder
    {
        internal static SquarePoint GetNextStep(int pUserX, int pUserY,
            int pUserTargetX, int pUserTargetY,
            SqState[,] pGameMap, double[,] pHeight,
            int MaxX, int MaxY,
            bool pUserOverride, bool pDiagonal)
        {
            ModelInfo MapInfo = new ModelInfo(MaxX, MaxY, pGameMap);
            SquarePoint TargetPoint = new SquarePoint(pUserTargetX, pUserTargetY, pUserTargetX, pUserTargetY, MapInfo.GetState(pUserTargetX, pUserTargetY), pUserOverride);
            if (pUserX == pUserTargetX && pUserY == pUserTargetY) //User is allready standing on its target
                return TargetPoint;

            SquareInformation SquareOnUser = new SquareInformation(pUserX, pUserY, TargetPoint, MapInfo, pUserOverride, pDiagonal);

            //if (!TargetPoint.CanWalk)
            //    return SquareOnUser.Point;

            return GetClosetSqare(SquareOnUser, new HeightInfo(MaxX, MaxY, pHeight));
        }

        private static SquarePoint GetClosetSqare(SquareInformation pInfo, HeightInfo Height)
        {
            double Closest = pInfo.Point.GetDistance; //Initialized

            SquarePoint ClosestPoint = pInfo.Point;
            double InfoOnSqare = Height.GetState(pInfo.Point.X, pInfo.Point.Y);

            for (int i = 0; i < 8; i++)
            {
                SquarePoint Position = pInfo.Pos(i);
                if (!Position.InUse)
                    continue;

                if (Position.CanWalk)
                {
                    if (Height.GetState(Position.X, Position.Y) - InfoOnSqare < 3)
                    {
                        double Distance = Position.GetDistance;
                        if (Closest > Distance)
                        {
                            Closest = Distance;
                            ClosestPoint = Position;
                        }
                    }
                }
            }
            return ClosestPoint;
        }

        internal static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }

    struct ModelInfo
    {
        private SqState[,] mMap;
        private int mMaxX;
        private int mMaxY;

        internal ModelInfo(int MaxX, int MaxY, SqState[,] Map)
        {
            mMap = Map;
            mMaxX = MaxX;
            mMaxY = MaxY;
        }

        internal SqState GetState(int x, int y)
        {
            if (x >= mMaxX || x < 0)
                return 0;
            if (y >= mMaxY || y < 0)
                return 0;
            return mMap[x, y];
        }
    }

    struct HeightInfo
    {
        private double[,] mMap;
        private int mMaxX;
        private int mMaxY;

        internal HeightInfo(int MaxX, int MaxY, double[,] Map)
        {
            mMap = Map;
            mMaxX = MaxX;
            mMaxY = MaxY;
        }

        internal double GetState(int x, int y)
        {
            if (x >= mMaxX || x < 0)
                return 0;
            if (y >= mMaxY || y < 0)
                return 0;
            return mMap[x, y];
        }
    }

    struct SquareInformation
    {
        #region Fields
        private int mX;
        private int mY;

        private SquarePoint[] mPos;

        private SquarePoint mTarget;
        private SquarePoint mPoint;
        #endregion

        #region Constructor
        internal SquareInformation(int pX, int pY, SquarePoint pTarget, ModelInfo pMap, bool pUserOverride, bool CalculateDiagonal)
        {
            mX = pX;
            mY = pY;
            mTarget = pTarget;

            mPoint = new SquarePoint(pX, pY, pTarget.X, pTarget.Y, pMap.GetState(pX, pY), pUserOverride);

            //Analyze all the squares arround the user
            mPos = new SquarePoint[8];

            if (CalculateDiagonal)
            {
                mPos[1] = new SquarePoint(pX - 1, pY - 1, pTarget.X, pTarget.Y, pMap.GetState(pX - 1, pY - 1), pUserOverride);
                mPos[3] = new SquarePoint(pX - 1, pY + 1, pTarget.X, pTarget.Y, pMap.GetState(pX - 1, pY + 1), pUserOverride);
                mPos[5] = new SquarePoint(pX + 1, pY + 1, pTarget.X, pTarget.Y, pMap.GetState(pX + 1, pY + 1), pUserOverride);
                mPos[7] = new SquarePoint(pX + 1, pY - 1, pTarget.X, pTarget.Y, pMap.GetState(pX + 1, pY - 1), pUserOverride);
            }

            mPos[0] = new SquarePoint(pX, pY - 1, pTarget.X, pTarget.Y, pMap.GetState(pX, pY - 1), pUserOverride);
            mPos[2] = new SquarePoint(pX - 1, pY, pTarget.X, pTarget.Y, pMap.GetState(pX - 1, pY), pUserOverride);
            mPos[4] = new SquarePoint(pX, pY + 1, pTarget.X, pTarget.Y, pMap.GetState(pX, pY + 1), pUserOverride);
            mPos[6] = new SquarePoint(pX + 1, pY, pTarget.X, pTarget.Y, pMap.GetState(pX + 1, pY), pUserOverride);

            ///456
            //Y3X7
            ///218
            ////X
        }

        //GetState( , pMap, pMaxX, pMaxY
        //private byte GetState(int pX, int pY, byte[,] pMap, int maxX, int maxY)
        //{
        //    if (pX >= maxX)
        //        return 0;
        //    if (pY >= maxY)
        //        return 0;

        //    return pMap[maxX, maxY];
        //}
        #endregion

        #region Return values
        //internal int X
        //{
        //    get
        //    {
        //        return mX;
        //    }
        //    set
        //    {
        //        mX = value;
        //    }
        //}

        //internal int Y
        //{
        //    get
        //    {
        //        return mY;
        //    }
        //    set
        //    {
        //        mY = value;
        //    }
        //}

        internal SquarePoint Pos(int val)
        {
            return mPos[val];
        }

        //internal SquarePoint Target
        //{
        //    get
        //    {
        //        return mTarget;
        //    }
        //}

        internal SquarePoint Point
        {
            get
            {
                return mPoint;
            }
        }
        #endregion
    }

    struct SquarePoint
    {
        #region Fields
        private int mX;
        private int mY;
        private double mDistance;
        private SqState mSquareData;
        private bool mInUse;
        private bool mOverride;
        private bool mLastStep;
        #endregion

        #region Constructor
        internal SquarePoint(int pX, int pY, int pTargetX, int pTargetY, SqState SquareData, bool pOverride)
        {
            mX = pX;
            mY = pY;
            mSquareData = SquareData;
            mInUse = true;
            mOverride = pOverride;

            mDistance = 0.0;
            mLastStep = (pX == pTargetX && pY == pTargetY);

            mDistance = DreamPathfinder.GetDistance(pX, pY, pTargetX, pTargetY);

        }
        #endregion

        #region Return values
        internal int X
        {
            get
            {
                return mX;
            }
        }

        internal int Y
        {
            get
            {
                return mY;
            }
        }

        internal double GetDistance
        {
            get
            {
                return mDistance;
            }
        }

        internal bool CanWalk
        {
            get
            {
                if (!mLastStep)
                {
                    if (!mOverride) return (mSquareData == SqState.Walkable || mSquareData == SqState.Idk);
                    else return true;
                }
                else
                {
                    if (!mOverride)
                    {
                        if (mSquareData == SqState.WalkableLast)
                            return true;
                        if (mSquareData == SqState.Walkable)
                            return true;
                    }
                    else return true;
                }
                return false;
            }
        }

        internal bool InUse
        {
            get
            {
                return mInUse;
            }
        }
        #endregion
    }
}
