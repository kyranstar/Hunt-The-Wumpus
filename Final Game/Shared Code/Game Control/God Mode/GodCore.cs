using System;
using System.Linq;


#if DESKTOP
using Tharga.Toolkit.Console;
using Tharga.Toolkit.Console.Command;
using Tharga.Toolkit.Console.Command.Base;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.GUI;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.GameControl
{
    public class GodManager
    {
        public Map Map;
        public GameController GameController;
        public MapRenderer MapRenderer;

        public GodManager(GameController GameController, MapRenderer MapRenderer)
        {
            this.GameController = GameController;
            this.Map = GameController.Map;
            this.MapRenderer = MapRenderer;
        }

        public void Initialize()
        {
            RootCommand Root = new RootCommand(Log.Console);

            Root.RegisterCommand(new GoToRoomCommand(Map));
            Root.RegisterCommand(new RoomInfoCommand(Map));
            Root.RegisterCommand(new ListRoomsCommand(Map));
            Root.RegisterCommand(new SetViewCommand(MapRenderer));
            Root.RegisterCommand(new OutlineCommand(MapRenderer));
            Root.RegisterCommand(new Display(GameController));
            Root.RegisterCommand(new DisableCommand(MapRenderer));

            CommandEngine Engine = new CommandEngine(Root);
            Engine.Run(new string[0]);

        }
    }
}

#endif