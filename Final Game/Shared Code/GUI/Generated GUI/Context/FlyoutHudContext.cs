using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Controls;
using HuntTheWumpus.SharedCode.Trivia;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class FlyoutHudContext
    {
        private GameController GameController;

        public const string FlyoutVisibilityGroup = "FlyoutVisibility";
        public const string FlyoutDataGroup = "FlyoutData";

        Action<string, string> RaisePropertyChangedForGroup;

        public FlyoutHudContext(GameController gameController, Action<string, string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            GameController = gameController;

            GameController.OnNewSecretAvailable += GameController_OnNewSecretAvailable;
            GameController.OnNewHintAvailable += GameController_OnNewHintAvailable;


            ShowFlyoutCommand = new RelayCommand(new Action<object>(ShowFlyout));
            BuySecretCommand = new RelayCommand(new Action<object>(BuySecret));
            BuyArrowCommand = new RelayCommand(new Action<object>(BuyArrow));

            FlyoutVisibility = Visibility.Hidden;
        }

        void GameController_OnNewSecretAvailable(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(FlyoutDataGroup, null);
        }

        private void GameController_OnNewHintAvailable(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(FlyoutDataGroup, null);
        }

        public ICommand ShowFlyoutCommand
        {
            get;
            protected set;
        }

        public ICommand BuySecretCommand
        {
            get;
            protected set;
        }

        public ICommand BuyArrowCommand
        {
            get;
            protected set;
        }

        public void ShowFlyout(object o)
        {
            if (FlyoutVisibility == Visibility.Visible)
                FlyoutVisibility = Visibility.Hidden;
            else
                FlyoutVisibility = Visibility.Visible;

            RaisePropertyChangedForGroup(FlyoutVisibilityGroup, null);
        }

        public void BuySecret(object o)
        {
            GameController.LoadNewTrivia(TriviaQuestionState.PurchasingSecret, 3);
        }

        public void BuyArrow(object o)
        {
            GameController.LoadNewTrivia(TriviaQuestionState.PurchasingArrow, 3);
        }


        [PropertyGroup(FlyoutVisibilityGroup)]
        public Visibility FlyoutVisibility
        {
            get;
            set;
        }

        [PropertyGroup(FlyoutDataGroup)]
        public string[] UnlockedHints
        {
            get
            {
                return Trivia.Trivia.AvailableHints.ToArray();
            }
        }

        [PropertyGroup(FlyoutDataGroup)]
        public string[] UnlockedSecrets
        {
            get
            {
                return GameController.SecretMan.UnlockedSecrets.Select(s => s.SecretText).ToArray();
            }
        }

        public void Reset()
        {
            FlyoutVisibility = Visibility.Hidden;
        }


        public void Update(GameTime gameTime)
        {

        }
    }
}
