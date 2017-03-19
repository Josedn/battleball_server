namespace BattleBall.Core.GameClients
{
    class ClientOpCodes
    {
        public const int LOGIN = 1;
        public const int REQUEST_MAP = 2;
        public const int REQUEST_MOVEMENT = 7;
        public const int REQUEST_CHAT = 9;
    }
    class ServerOpCodes
    {
        public const int LOGIN_OK = 3;
        public const int MAP_DATA = 4;
        public const int PLAYERS_DATA = 6;
        public const int PLAYER_MOVEMENT = 8;
        public const int CHAT = 10;
    }
}
