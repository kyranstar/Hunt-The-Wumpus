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
    class OutlineCommand : ActionCommandBase
    {
        private const string Help = "Controls the positioning and visibility of the debug outline.";

        MapRenderer MapRenderer;
        public OutlineCommand(MapRenderer MapRenderer)
            : base("outline", Help)
        {
            this.MapRenderer = MapRenderer;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            if (paramList == null || paramList.Length <= 0 || paramList == "none")
            {
                MapRenderer.DebugOutline = null;
                return true;
            }

            switch (paramList)
            {
                case "viewport":
                    MapRenderer.DebugOutline = MapRenderer.MapCam.VirtualVisibleViewport.ToRect(-1, -1);
                    break;
                default:
                    this.OutputError("Outline mode not found.");
                    break;
            }

            return true;
        }
    }
}

#endif