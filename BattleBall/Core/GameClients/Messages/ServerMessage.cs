namespace BattleBall.Core.GameClients.Messages
{
    class ServerMessage
    {
        private string Body;

        internal ServerMessage(int Id)
        {
            Body = Id.ToString();
        }

        public void AppendInt(int i)
        {
            AppendString(i.ToString());
        }

        public void AppendString(string str)
        {
            Body += "|" + str;
        }

        public override string ToString()
        {
            return Body;
        }
    }
}
