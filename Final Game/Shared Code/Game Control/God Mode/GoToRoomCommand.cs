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
    class GoToRoomCommand : ActionCommandBase
    {
        private const string Help = "Moves the player to the specified room.";

        Map Map;
        public GoToRoomCommand(Map Map)
            : base("mv", Help)
        {
            this.Map = Map;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            string param = GetParam(paramList, 0);
            if (param.Equals("wumpus", StringComparison.OrdinalIgnoreCase))
            {
                return Map.MovePlayerTo(Map.Wumpus.Location);
            }

            int ID = int.Parse(param);
            return Map.MovePlayerTo(ID);
        }
    }
}

#endif