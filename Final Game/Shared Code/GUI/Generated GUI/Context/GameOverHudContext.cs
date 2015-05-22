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
using HuntTheWumpus.SharedCode.Scores;

namespace HuntTheWumpus.SharedCode.GUI
{
    class GameOverHudContext
    {
        private GameController GameController;

        private StateAnimator GameOverModalMarginAnimation;
        private float PreviousNotifiedGameOverMargin = 0;

        private StateAnimator GameOverModalOpacityAnimation;
        private float PreviousNotifiedGameOverOpacity = 0;

        public const string GameOverBindingGroup = "GameOverBinding";
        public const string GameOverVisibilityGroup = "GameOverVisibility";

        Action<string> RaisePropertyChangedForGroup;

        public GameOverHudContext(GameController gameController, Action<string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            GameController = gameController;
            GameController.OnGameOver += GameController_OnGameOver;

            ReturnToMenuCommand = new RelayCommand(new Action<object>(ReturnToMenu));
            SubmitNameCommand = new RelayCommand(new Action<object>(SubmitName));

            GameOverModalMarginAnimation = new StateAnimator(
                Pct =>
                {
                    // TODO: Add math to do cubic Bezier curve:
                    // (0.165, 0.84), (0.44, 1)
                    return (float)(Math.Pow(-Pct + 1, 2) * 200);
                },
                Pct =>
                {
                    // Don't need to handle this -- should never fade out
                    return 0;
                },
            1);

            GameOverModalOpacityAnimation = new StateAnimator(
                Pct =>
                {
                    // TODO: Add math to do cubic Bezier curve:
                    // (0.165, 0.84), (0.44, 1)
                    return (float)Math.Pow(Pct, 2);
                },
                Pct =>
                {
                    // Don't need to handle this -- should never fade out
                    return 0;
                },
            0.6);
        }

        public void Reset()
        {
            GameOverModalMarginAnimation.Reset();
            PreviousNotifiedGameOverMargin = 0;

            GameOverModalOpacityAnimation.Reset();
            PreviousNotifiedGameOverOpacity = 0;
        }

        private void GameController_OnGameOver(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(GameOverBindingGroup);
        }

        public ICommand ReturnToMenuCommand
        {
            get;
            protected set;
        }

        public ICommand SubmitNameCommand
        {
            get;
            protected set;
        }

        public bool IsGameOver
        {
            get
            {
                return GameController.GameOverState != null;
            }
        }

        [PropertyGroup(GameOverVisibilityGroup)]
        public float GameOverModalOpacity
        {
            get
            {
                return MathHelper.Clamp(GameOverModalOpacityAnimation.CurrentValue, 0, 1);
            }
        }

        [PropertyGroup(GameOverVisibilityGroup)]
        public Thickness GameOverModalMargin
        {
            get
            {
                return new Thickness(GameOverModalMarginAnimation.CurrentValue, 0, -GameOverModalMarginAnimation.CurrentValue, 0);
            }
        }

        [PropertyGroup(GameOverVisibilityGroup)]
        public Visibility GameOverModalVisibility
        {
            get
            {
                return GameOverModalOpacity > 0.01 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string GameOverUsernameText
        {
            get; set;
        }

        [PropertyGroup(GameOverBindingGroup)]
        public string GameOverMessage
        {
            get
            {
                if (!IsGameOver)
                    return null;

                switch (GameController.GameOverState.Cause)
                {
                    case GameOverCause.FellInPit:
                        return "You were trapped in a pit. You lose!";
                    case GameOverCause.HitWumpus:
                        return "You ran into the Wumpus and were killed. You lose!";
                    case GameOverCause.NoArrows:
                        return "You ran out of arrows. You lose!";
                    case GameOverCause.ShotWumpus:
                        return "You shot the Wumpus! You win!";
                }

                return null;
            }
        }

        public void Update(GameTime gameTime)
        {
            GameOverModalMarginAnimation.Update(gameTime, IsGameOver);
            if (MathHelper.Distance(GameOverModalMargin.Left, PreviousNotifiedGameOverMargin) > 0.01)
            {
                RaisePropertyChangedForGroup(GameOverVisibilityGroup);
                PreviousNotifiedGameOverMargin = GameOverModalMargin.Left;
            }


            GameOverModalOpacityAnimation.Update(gameTime, IsGameOver);
            if (MathHelper.Distance(GameOverModalOpacity, PreviousNotifiedGameOverOpacity) > 0.01)
            {
                RaisePropertyChangedForGroup(GameOverVisibilityGroup);
                PreviousNotifiedGameOverOpacity = GameOverModalOpacity;
                Log.Info("Opacity: " + GameOverModalOpacityAnimation.CurrentValue);
            }

        }

        private void ReturnToMenu(object o)
        {
            SceneManager.LoadScene(SceneManager.MenuScene);
        }

        private void SubmitName(object o)
        {
            ScoreManager ScoreMan = new ScoreManager();
            ScoreMan.Load();

            ScoreEntry PlayerScore = GameController.Map.Player.ScoreEntry;
            PlayerScore.Username = GameOverUsernameText;
            PlayerScore.ScoreDate = DateTime.Now;

            ScoreMan.AddScore(PlayerScore);

            ScoreMan.Save();
        }
    }
}
