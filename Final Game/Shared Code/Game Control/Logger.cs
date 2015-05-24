using System;
using System.Diagnostics;

#if DESKTOP
using Tharga.Toolkit.Console.Command.Base;
using HuntTheWumpus.SharedCode.Helpers;
#endif

namespace HuntTheWumpus.SharedCode.GameControl
{
    public static class Log
    {
#if DESKTOP
        public static IConsole Console = new ClientConsole();
#endif

        public static void Info(string Message)
        {
#if DESKTOP
            PrintSimpleStack();
            WriteToConsole("INFO:", ConsoleColor.White, ConsoleColor.DarkCyan);
            WriteLineToConsole(" " + Message, ConsoleColor.Cyan, ConsoleColor.Black);
#endif
            Debug.WriteLine("INFO: " + Message);
        }

        public static void Warn(string Message)
        {
#if DESKTOP
            PrintSimpleStack();
            WriteToConsole("WARN:", ConsoleColor.White, ConsoleColor.DarkYellow);
            WriteLineToConsole(" " + Message, ConsoleColor.Yellow, ConsoleColor.Black);
#endif
            Debug.WriteLine("WARN: " + Message);
        }

        public static void Error(string Message)
        {
#if DESKTOP
            PrintSimpleStack();
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
            try
            { 
                Console.WriteLine(Message);
            }
            catch
            {
                
            }

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

        private static void PrintSimpleStack()
        {
            string ClassName = ReflectionUtils.GetCallerClass(framesUp: 2).Name;
            string MethodName = ReflectionUtils.GetCallerMethod(framesUp: 2).Name;
            WriteLineToConsole(ClassName + ":" + MethodName + " - ", ConsoleColor.White, ConsoleColor.DarkRed);
        }
#endif
    }
}
