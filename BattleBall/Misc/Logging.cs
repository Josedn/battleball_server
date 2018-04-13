using System;

namespace BattleBall.Misc
{
    public enum LogLevel
    {
        SuperDebug,
        Debug,
        Verbose,
        Warning,
        Info,
        None,
    }

    public static class Logging
    { 
        public static LogLevel LogLevel = LogLevel.Verbose;

        public static void SetLogLevel(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }

        public static void WriteLine(object o, ConsoleColor c, LogLevel logLevel)
        {
            if (LogLevel <= logLevel)
            {
                if (Console.ForegroundColor != c)
                    Console.ForegroundColor = c;
                Console.WriteLine("[" + logLevel.ToString().ToUpper() + "] - " + o);
                ResetColor();
            }
        }

        public static void WriteLine(object o, LogLevel logLevel)
        {
            WriteLine(o, ConsoleColor.Gray, LogLevel);
        }
    }
}
