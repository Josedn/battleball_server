using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBall.Core.GameClients
{
    class ClientOpCodes
    {
        public const int LOGIN = 1;
        public const int REQUEST_MAP = 2;
    }
    class ServerOpCodes
    {
        public const int LOGIN_OK = 3;
        public const int MAP_DATA = 4;
    }
}
