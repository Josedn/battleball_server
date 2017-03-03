namespace BattleBall.AStar.Algorithm
{
    public interface IWeightAlterable<T>
    {
        T Weight { get; set; }
    }
}