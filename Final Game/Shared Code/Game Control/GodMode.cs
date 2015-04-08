using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#if DESKTOP
using Tharga.Toolkit.Console; 
using Tharga.Toolkit.Console.Command; 
using Tharga.Toolkit.Console.Command.Base;
using HuntTheWumpus.SharedCode.GameMap;
using Rug.Cmd; 

namespace HuntTheWumpus.SharedCode.GameControl
{
    public class GodManager
    {
        public Map Map;

        public void Initialize()
        {
            RootCommand Root = new RootCommand(Log.Console);

            Root.RegisterCommand(new GoToRoomCommand(Map));
            Root.RegisterCommand(new RoomCommand(Map));

            CommandEngine Engine = new CommandEngine(Root);
            Engine.Run(new string[0]);

        }
    }

    class GoToRoomCommand : ActionCommandBase
    {
        private const string Help = "Moves the player to the specified room.";

        Map Map;
        public GoToRoomCommand(Map Map)
            : base("moveto", Help)
        {
            this.Map = Map;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
 	        int ID = int.Parse(GetParam(paramList, 0));
            return Map.MovePlayerTo(ID);
        }
    }

    class RoomCommand : ActionCommandBase
    {
        private const string Help = "Gets or sets information about the specified room, or the current one if no room is specified.";

        private StringArgument PropertyName;
        private StringArgument PropertyValue;

        Map Map;
        public RoomCommand(Map Map)
            : base("room", Help)
        {
            this.Map = Map;
            PropertyName = new StringArgument("property", "The name of the target property", "The name of the target property");

        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            int ID;
            if(!int.TryParse(GetParam(paramList, 0), out ID))
                ID = Map.PlayerRoom;

            ArgumentParser Parser = new ArgumentParser("Hunt the Wumpus", "\"God Mode\"");
            // TODO: Be cognicent of quotes
            Parser.Parse(paramList.Split(' '));
            // TODO: Do something with the parsed info...
            OutputInformation("Room info: {0}", Map.Cave.GetRoom(Map.PlayerRoom));

            return true;
        }
    }


}

#endif