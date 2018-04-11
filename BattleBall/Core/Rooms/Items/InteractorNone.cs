using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.Rooms.Items
{
    class InteractorNone : RoomItemInteractor
    {
        public InteractorNone(RoomItem item) : base(item)
        {

        }

        public override void OnTrigger(RoomUser roomUser, bool userHasRights)
        {
            
        }
    }
}
