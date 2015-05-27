using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class SecretManager
    {
        private Random Rand = new Random();
        private GameController GameController;
        private List<Secret> _UnlockedSecrets;

        public SecretManager(GameController gameController)
        {
            GameController = gameController;
            _UnlockedSecrets = new List<Secret>();
        }

        public Secret[] UnlockedSecrets
        {
            get
            {
                return _UnlockedSecrets.ToArray();
            }
        }

        public Secret UnlockNewSecret()
        {

            Secret NewSecret = new Secret
            {
                TimeCreated = GameController.Map.MoveCount,
                SecretText = GetRandomSecret()
            };

            _UnlockedSecrets.Add(NewSecret);
            return NewSecret;
        }
        /// <summary>
        /// Gives a random secret in string form
        /// </summary>
        /// <returns></returns>
        private string GetRandomSecret()
        {
            Cave cave = GameController.Map.Cave;

            switch (Rand.Next(4))
            {
                case 0: // How many tiles away the nearest bats are
                    return String.Format("The nearest bats are {0} tiles away",
                        cave.Rooms.
                            Where(r => r.HasBats).
                            Select(r => Pathfinding.FindPath(r, cave[GameController.Map.PlayerRoom], cave, false).Count).
                            OrderBy(r => r).
                            First());
                case 1: // How many tiles away the nearest pit is
                    return String.Format("The nearest pit is {0} tiles away",
                        cave.Rooms.
                            Where(r => r.HasPit).
                            Select(r => Pathfinding.FindPath(r, cave[GameController.Map.PlayerRoom], cave, false).Count).
                            OrderBy(r => r).
                            First());
                case 2: // The room number of the wumpus
                    return String.Format("The wumpus' room number is {0}",
                        GameController.Map.Wumpus.Location);
                case 3: // Current player room number
                    return String.Format("The player's room number is {0}",
                        GameController.Map.PlayerRoom);

                default: throw new Exception();
            }
        }
    }

    public class Secret
    {
        public string SecretText;
        public int TimeCreated;
    }
}
