﻿namespace BattleBall.AStar.Algorithm
{
    public interface IPathNode
    {
        bool IsBlocked(int x, int y, bool lastTile);
    }
}