namespace BattleBall.Core.Items
{
    class BaseItem
    {
        internal int BaseId;
        internal int X;
        internal int Y;
        internal double Z;
        internal string AssetName;

        public BaseItem(int baseId, int x, int y, double z, string assetName)
        {
            BaseId = baseId;
            X = x;
            Y = y;
            Z = z;
            AssetName = assetName;
        }
    }
}
