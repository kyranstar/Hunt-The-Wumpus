using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuntTheWumpus.SharedCode.GameControl;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class SecretManager
    {
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
                SecretText = "INSERT SECRET INFO HERE"
            };

            // TODO: Return a secret that gives some sort of contextual help

            _UnlockedSecrets.Add(NewSecret);
            return NewSecret;
        }
    }

    public class Secret
    {
        public string SecretText;
        public int TimeCreated;
    }
}
