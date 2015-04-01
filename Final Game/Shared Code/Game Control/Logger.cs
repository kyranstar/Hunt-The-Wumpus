using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.GameControl
{
    static class Log
    {
        public static void Info(string Message)
        {
#if !NETFX_CORE
            WriteToConsole("INFO:", ConsoleColor.White, ConsoleColor.DarkCyan);
            WriteLineToConsole(" " + Message, ConsoleColor.Cyan, ConsoleColor.Black);
#endif
            Debug.WriteLine("INFO: " + Message);
        }

        public static void Warn(string Message)
        {
#if !NETFX_CORE
            WriteToConsole("WARN:", ConsoleColor.White, ConsoleColor.DarkYellow);
            WriteLineToConsole(" " + Message, ConsoleColor.Yellow, ConsoleColor.Black);
#endif
            Debug.WriteLine("WARN: " + Message);
        }

        public static void Error(string Message)
        {
#if !NETFX_CORE
            WriteToConsole("ERR:", ConsoleColor.White, ConsoleColor.DarkRed);
            WriteLineToConsole(" " + Message, ConsoleColor.Red, ConsoleColor.Black);
#endif
            Debug.WriteLine("ERR: " + Message);
        }

        
#if !NETFX_CORE
        private static void WriteLineToConsole(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.WriteLine(Message);

            Debug.WriteLine(Message);
        }

        private static void WriteToConsole(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(Message);
        }
#endif
    }
}
