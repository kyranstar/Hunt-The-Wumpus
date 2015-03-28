using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.GameControl
{
    static class Log
    {
#if !NETFX_CORE
        public static void Info(string Message)
        {
            WriteWithColor("INFO:", ConsoleColor.White, ConsoleColor.DarkCyan);
            WriteLineWithColor(" " + Message, ConsoleColor.Cyan, ConsoleColor.Black);
        }

        public static void Warn(string Message)
        {
            WriteWithColor("WARN:", ConsoleColor.White, ConsoleColor.DarkYellow);
            WriteLineWithColor(" " + Message, ConsoleColor.Yellow, ConsoleColor.Black);
        }

        public static void Error(string Message)
        {
            WriteWithColor("ERR:", ConsoleColor.White, ConsoleColor.DarkRed);
            WriteLineWithColor(" " + Message, ConsoleColor.Red, ConsoleColor.Black);
        }

        private static void WriteLineWithColor(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.WriteLine(Message);
        }

        private static void WriteWithColor(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(Message);
        }
#else
        public static void Info(string Message)
        {
            // TODO: Log in modern environment
        }

        public static void Warn(string Message)
        {
            // TODO: Log in modern environment
        }

        public static void Error(string Message)
        {
            // TODO: Log in modern environment
        }
#endif
    }
}
