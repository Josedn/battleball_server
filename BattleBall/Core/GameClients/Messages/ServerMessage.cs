namespace BattleBall.Core.GameClients.Messages
{
    class ServerMessage
    {
        private string Body;
        private const char SEPARATOR = '|';

        internal ServerMessage(int Id)
        {
            Body = Id.ToString();
        }

        private void AppendToken(string token)
        {
            Body += SEPARATOR + token;
        }

        public void AppendInt(int i)
        {
            AppendToken(i.ToString());
        }

        public void AppendString(string str)
        {
            int tickets = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == SEPARATOR)
                    tickets++;
            }
            AppendInt(tickets);
            AppendToken(str);
        }

        public override string ToString()
        {
            return Body;
        }
    }
}
