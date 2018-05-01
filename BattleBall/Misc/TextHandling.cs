namespace BattleBall.Misc
{
    static class TextHandling
    {
        internal static string GetString(double k)
        {
            return k.ToString().Replace(',', '.');
        }
    }
}
