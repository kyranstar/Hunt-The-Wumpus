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
    class DisplayCommand : ActionCommandBase
    {
        private const string Hazards = "hazards";
        private const string Rooms = "rooms";
        private const string GameOver = "gameover";

        private const string Help =
@"Graphically displays whatever is passed in as a parameter:
        - " + Hazards + @": Displays pits, bats, and the wumpus in their location.
        - " + Rooms + @": Sets all rooms as visible and removes fog of war.
        - " + GameOver + @": Displays the gameover screen.";


        GameController GameController;
        public DisplayCommand(GameController GameController)
            : base("display", Help)
        {
            this.GameController = GameController;
        }

        public override async System.Threading.Tasks.Task<bool> InvokeAsync(string paramList)
        {
            string toDisplay = GetParam(paramList, 0);
            switch (toDisplay)
            {
                case Hazards:
                    foreach (int room in GameController.Map.Cave.RoomDict.
                        Where(r => r.Value.HasPit || r.Value.HasBats).
                        Select(r => r.Key))
                    {
                        GameController.Map.PlayerPath.Enqueue(room);
                        GameController.Map.MoveCount++;
                    }
                    break;
                case Rooms:
                    foreach (int room in GameController.Map.Cave.RoomDict.Keys)
                    {
                        GameController.Map.PlayerPath.Enqueue(room);
                        GameController.Map.MoveCount++;
                    }
                    break;
                case GameOver:
                    GameController.EndGame(Scores.GameOverCause.HitWumpus);
                    break;
            }
            return true;
        }
    }
}

#endif