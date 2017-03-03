using System;

namespace BattleBall.Core.GameClients.Messages
{
    class ClientMessage
    {
        private string Body;
        private string[] Tokens;
        private int Pointer;

        private int id;

        internal ClientMessage(string Message)
        {
            this.Pointer = 0;
            this.id = -1;
            this.Body = Message;
            this.Tokens = Body.Split('|');

            if (!int.TryParse(PopString(), out id))
            {
                throw new ArgumentException("Invalid MessageId!!");
            }
        }

        internal int Id
        {
            get
            {
                return id;
            }
        }

        internal string PopString()
        {
            if (Tokens.Length > Pointer)
            {
                return Tokens[Pointer++];
            }
            return null;
        }

    }
}
