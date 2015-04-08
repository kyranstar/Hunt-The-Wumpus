#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

using HuntTheWumpus.SharedCode.GameCore;
using HuntTheWumpus.SharedCode.GameControl;
using Tharga.Toolkit.Console.Command.Base;
#endregion

namespace HuntTheWumpus.Platform.Desktop
{
#if DESKTOP
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IConsole Console = new ClientConsole();
            Log.Console = Console;

            Log.Info("Running game...");
            using (var game = new GameHost())
            {
#if DEBUG
                game.Run();
#else
                try
                {
                    game.Run();
                }
                catch (Exception e)
                {
                    Log.Error("Game threw exception: " + e);
                    Console.Read();
                }
#endif
            }
        }
    }
#endif
}
