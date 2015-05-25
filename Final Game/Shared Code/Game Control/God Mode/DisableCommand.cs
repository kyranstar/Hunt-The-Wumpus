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
    class DisableCommand : ActionCommandBase
    {
        private const string Particles = "particles";

        private const string Help =
@"Disables whatever is passed in as a parameter:
        - " + Particles + @": Disables particle systems.";

        MapRenderer MapRenderer;
        public DisableCommand(MapRenderer MapRenderer)
            : base("disable", Help)
        {
            this.MapRenderer = MapRenderer;

        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            switch (GetParam(paramList, 0))
            {
                case Particles:
                    MapRenderer.ParticleSystemsEnabled = false;
                    break;
            }
            return true;
        }
    }


}

#endif