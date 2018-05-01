using BattleBall.Communication.Protocol;
using BattleBall.Core.Rooms;

namespace BattleBall.Communication.Outgoing.Rooms
{
    class MapComposer : ServerMessage
    {
        public MapComposer(MapModel model) : base(ServerOpCodes.MAP_DATA)
        {
            AppendInt(model.MaxX);
            AppendInt(model.MaxY);
            AppendInt(model.DoorX);
            AppendInt(model.DoorY);

            for (int i = 0; i < model.MaxX; i++)
            {
                for (int j = 0; j < model.MaxY; j++)
                {
                    AppendInt(model.Map[i, j]);
                }
            }
        }
    }
}
