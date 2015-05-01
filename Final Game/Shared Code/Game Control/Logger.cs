using System;
using System.Diagnostics;

#if DESKTOP
using Tharga.Toolkit.Console.Command.Base;
using HuntTheWumpus.SharedCode.Helpers;
#endif

namespace HuntTheWumpus.SharedCode.GameControl
{
    static class Log
    {
#if DESKTOP
        public static IConsole Console = new ClientConsole();
#endif

        public static void Info(string Message)
        {
            PrintCallerClass();
#if DESKTOP
            WriteToConsole("INFO:", ConsoleColor.White, ConsoleColor.DarkCyan);
            WriteLineToConsole(" " + Message, ConsoleColor.Cyan, ConsoleColor.Black);
#endif
            Debug.WriteLine("INFO: " + Message);
        }

        public static void Warn(string Message)
        {
            PrintCallerClass();
#if DESKTOP
            WriteToConsole("WARN:", ConsoleColor.White, ConsoleColor.DarkYellow);
            WriteLineToConsole(" " + Message, ConsoleColor.Yellow, ConsoleColor.Black);
#endif
            Debug.WriteLine("WARN: " + Message);
        }

        public static void Error(string Message)
        {
            PrintCallerClass();
#if DESKTOP
            WriteToConsole("ERR:", ConsoleColor.White, ConsoleColor.DarkRed);
            WriteLineToConsole(" " + Message, ConsoleColor.Red, ConsoleColor.Black);
#endif
            Debug.WriteLine("ERR: " + Message);
        }


#if DESKTOP
        private static void WriteLineToConsole(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.WriteLine(Message);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void WriteToConsole(string Message, ConsoleColor Foreground, ConsoleColor Background)
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            // Console.Write(Message);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
#endif
        private static void PrintCallerClass()
        {
            string className = ReflectionUtils.GetCallerClass(framesUp: 2).Name;
#if DESKTOP
            WriteLineToConsole(className + " - ", ConsoleColor.White, ConsoleColor.DarkRed);
#endif
            Debug.WriteLine(className + " - ");
        }
    }
}
