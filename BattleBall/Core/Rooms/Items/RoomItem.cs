using System;
using BattleBall.Communication.Outgoing.Rooms;
using BattleBall.Communication.Protocol;
using BattleBall.Core.Items;

namespace BattleBall.Core.Rooms.Items
{
    class RoomItem
    {
        internal int ItemId;
        internal int X, Y, Rot;
        internal double Z;
        internal int State;
        public Room Room { get; }

        public bool NeedsUpdate { get; set; }
        internal BaseItem BaseItem;
        internal RoomItemInteractor Interactor;

        public RoomItem(int itemId, int x, int y, double z, int rot, Room room, BaseItem baseItem)
        {
            ItemId = itemId;
            X = x;
            Y = y;
            Rot = rot;
            Z = z;
            Room = room;
            BaseItem = baseItem;
            State = 0;
            NeedsUpdate = false;
            if (BaseItem.States > 1)
                Interactor = new InteractorGeneric(this);
            else
                Interactor = new InteractorNone(this);
        }

        internal void UpdateState()
        {
            NeedsUpdate = true;
            ServerMessage updateMessage = new FurniStateComposer(ItemId, State);
            Room.SendMessage(updateMessage);
        }
    }
}
