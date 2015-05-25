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
    class ListRoomsCommand : ActionCommandBase
    {
        private const string Help = "Lists all rooms.";

        Map Map;
        public ListRoomsCommand(Map Map)
            : base("li", Help)
        {
            this.Map = Map;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            foreach (Room Room in Map.Cave.Rooms)
                OutputInformation(Room.ToString().Replace("{", "{{").Replace("}", "}}"));
            return true;
        }
    }
}

#endif