using BattleBall.Core.GameClients;

namespace BattleBall.Core.Rooms.Items
{
    class InteractorGeneric : RoomItemInteractor
    {
        int States;
        public InteractorGeneric(RoomItem item) : base(item)
        {
            States = Item.BaseItem.States;
        }

        public override void OnTrigger(RoomUser roomUser, bool userHasRights)
        {
            if (Item.State + 1 < States)
            {
                Item.State++;
            }
            else
            {
                Item.State = 0;
            }
            Item.UpdateState();
        }
    }
}
