namespace BattleBall.Communication.Protocol
{
    class ServerOpCodes
    {
        public const int LOGIN_OK = 3;
        public const int MAP_DATA = 4;
        public const int PLAYERS_DATA = 6;
        public const int PLAYER_MOVEMENT = 8;
        public const int CHAT = 10;
        public const int PLAYER_REMOVE = 11;
        public const int PLAYER_WAVE = 14;
    }
}
