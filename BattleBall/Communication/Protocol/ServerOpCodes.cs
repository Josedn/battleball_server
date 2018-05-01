namespace BattleBall.Communication.Protocol
{
    class ServerOpCodes
    {
        public const int LOGIN_OK = 3;
        public const int MAP_DATA = 4;
        public const int PLAYERS_DATA = 6;
        public const int PLAYER_STATUS = 8;
        public const int CHAT = 10;
        public const int PLAYER_REMOVE = 11;
        public const int PLAYER_WAVE = 14;
        public const int ROOM_ITEM_DATA = 16;
        public const int ITEM_REMOVE = 17;
        public const int ITEM_STATE = 19;
        public const int WALL_ITEM_DATA = 20;
    }
}
