using System;
using System.Diagnostics;

#if DESKTOP
using Tharga.Toolkit.Console.Command.Base;
using HuntTheWumpus.SharedCode.Helpers;
#endif

namespace HuntTheWumpus.SharedCode.GameControl
{
    /// <summary>
    /// Provides easy access to write colorized
    /// output to the console and debug stream.
    /// </summary>
    public static class Log
    {
#if DESKTOP
        public static IConsole Console = new ClientConsole();
#endif

        /// <summary>
        /// Prints an informational string of text to the
        /// console and debug out.
        /// </summary>
        /// <param name="Message">The message to print</param>
        public static void Info(string Message)
        {
#if DESKTOP
            PrintSimpleStack();
            WriteToConsole("INFO:", ConsoleColor.White, ConsoleColor.DarkCyan);
            WriteLineToConsole(" " + Message, ConsoleColor.Cyan, ConsoleColor.Black);
#endif
            Debug.WriteLine("INFO: " + Message);
        }

        /// <summary>
        /// Prints a warning to the console and
        /// debug out stream. Should only be used
        /// for text that indicates a non-optimal
        /// state or a problem with functionality
        /// that does not prevent the game from
        /// operating.
        /// </summary>
        /// <param name="Message">The message to print</param>
        public static void Warn(string Message)
        {
#if DESKTOP
            PrintSimpleStack();
            WriteToConsole("WARN:", ConsoleColor.White, ConsoleColor.DarkYellow);
            WriteLineToConsole(" " + Message, ConsoleColor.Yellow, ConsoleColor.Black);
#endif
            Debug.WriteLine("WARN: " + Message);
        }

        /// <summary>
        /// Prints an error message to the console
        /// and debug out stream. Should only be
        /// used for text that indicates a faliure
        /// to complete a task that limits game
        /// functionality or prevents it from
        /// operating correctly.
        /// </summary>
        /// <param name="Message"></param>
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
            WriteLineToConsole(ClassName + ":" + MethodName + " - ", ConsoleColor.White, ConsoleColor.DarkGray);
        }
#endif
    }
}
