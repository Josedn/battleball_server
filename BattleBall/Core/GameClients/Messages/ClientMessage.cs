using System;
using System.Linq;

namespace BattleBall.Core.GameClients.Messages
{
    class ClientMessage
    {
        private string Body;
        private string[] Tokens;
        private int Pointer;
        private int id;

        private const char SEPARATOR = '|';

        internal ClientMessage(string Message)
        {
            this.Pointer = 0;
            this.id = -1;
            this.Body = Message;
            this.Tokens = Body.Split(SEPARATOR);

            if (!int.TryParse(PopToken(), out id))
            {
                throw new ArgumentException("Invalid MessageId!!");
            }
        }

        public int Id => id;

        private string PopToken()
        {
            if (Tokens.Length > Pointer)
            {
                return Tokens[Pointer++];
            }
            return null;
        }

        internal string PopString()
        {
            int tickets = PopInt();
            string totalString = PopToken();

            for (int i = 0; i < tickets; i++)
            {
                totalString += SEPARATOR + PopToken();
            }

            return totalString;
        }
        internal int PopInt()
        {
            try
            {
                return int.Parse(PopToken());
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }
    }
}
