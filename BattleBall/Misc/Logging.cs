using System;

namespace BattleBall.Misc
{
    class Logging
    {
        public static void ResetColor()
        {
            Console.ResetColor();
        }
        public static void WriteLine()
        {
            Console.WriteLine();
        }
        public static void WriteLine(object o, ConsoleColor c)
        {
            if (Console.ForegroundColor != c)
                Console.ForegroundColor = c;
            Console.WriteLine(o);
            ResetColor();
        }

        public static void Write(object o, ConsoleColor c)
        {
            if (Console.ForegroundColor != c)
                Console.ForegroundColor = c;
            Console.Write(o);
            ResetColor();
        }
    }
}
