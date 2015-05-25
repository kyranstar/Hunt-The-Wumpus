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
    class SetViewCommand : ActionCommandBase
    {
        private const string Help = "Sets the coordinates or zoom of the map camera to the specified value(s), or resets the camera to auto-follow if no arguments are given.";

        MapRenderer MapRenderer;
        public SetViewCommand(MapRenderer MapRenderer)
            : base("cam", Help)
        {
            this.MapRenderer = MapRenderer;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            if (paramList == null || paramList.Length <= 0)
            {
                MapRenderer.OverriddenCameraPosition = null;
                return true;
            }

            if (paramList.Contains(",") || paramList.Contains(" "))
            {
                int[] Coords = paramList.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s.Trim())).ToArray();
                MapRenderer.OverriddenCameraPosition = new Vector2(Coords[0], Coords[1]);
            }
            else
                MapRenderer.CameraZoom = float.Parse(paramList.Trim());

            return true;
        }
    }
}

#endif