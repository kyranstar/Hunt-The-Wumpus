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
    class RoomInfoCommand : ActionCommandBase
    {
        private const string Help = "Gets or sets information about the specified room, or the current one if no room is specified.";

        Map Map;
        public RoomInfoCommand(Map Map)
            : base("rinfo", Help)
        {
            this.Map = Map;

        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            string PropertyName = null, NewValue = null;

            int ID;
            if (!int.TryParse(GetParam(paramList, 0), out ID))
                ID = Map.PlayerRoom;

            if (paramList != null)
            {
                var Arguments = ArgumentParser.Parse(paramList);

                PropertyName = ArgumentParser.GetParam(Arguments, "property", "prop");
                NewValue = ArgumentParser.GetParam(Arguments, "value", "val"); ;

            }

            if (PropertyName != null && NewValue == null)
            {
                // Get a value
                OutputInformation(
                    "Room {0} property {1}: {2}",
                    ID,
                    PropertyName,
                    ReflectionUtils.GetPropertyOrField<Room>(Map.Cave.GetRoom(ID), PropertyName));
            }
            else if (PropertyName != null && NewValue != null)
            {
                // Set a value
                ReflectionUtils.SetPropertyOrField<Room>(Map.Cave.GetRoom(ID), PropertyName, NewValue);
            }
            else
            {
                // Give some basic info
                OutputInformation("Room info: {0}", Map.Cave.GetRoom(Map.PlayerRoom));
            }

            return true;
        }
    }
}

#endif