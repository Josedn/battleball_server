using BattleBall.Core.GameClients;

namespace BattleBall.Core.Rooms.Items
{
    abstract class RoomItemInteractor
    {
        public RoomItem Item;
        public RoomItemInteractor(RoomItem item)
        {
            Item = item;
        }
        public abstract void OnTrigger(RoomUser roomUser, bool userHasRights);
    }
}
