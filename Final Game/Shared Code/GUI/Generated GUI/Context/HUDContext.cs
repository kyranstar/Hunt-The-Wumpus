using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scores;
using System;
using EmptyKeys.UserInterface.Controls;
using System.Linq;
using Microsoft.Xna.Framework;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HUDContext : ViewModelBase
    {
        Player Player;

        private StateAnimator GameOverModalMarginAnimation;
        private float PreviousNotifiedGameOverMargin = 0;

        private StateAnimator GameOverModalOpacityAnimation;
        private float PreviousNotifiedGameOverOpacity = 0;

        GameController GameController;

        public ScoreHudContext ScoreContext { get; set; }
        public TriviaHudContext TriviaContext { get; set; }

        private const string GameOverBindingGroup = "GameOverBinding";
        private const string GameOverVisibilityGroup = "GameOverVisibility";
        private const string HintVisibilityGroup = "HintVisibility";

        public HUDContext(GameController GameController)
        {
            this.GameController = GameController;
            this.Player = GameController.Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            GameController.OnNewQuestion += Trivia_NewQuestion;
            GameController.OnGameOver += GameController_OnGameOver;

            ReturnToMenuCommand = new RelayCommand(new Action<object>(ReturnToMenu));
            ShowHintsCommand = new RelayCommand(new Action<object>(ShowHints));

            HintFlyoutVisibility = Visibility.Hidden;


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

            ScoreContext = new ScoreHudContext(Player);
            TriviaContext = new TriviaHudContext(GameController, RaisePropertyChangedForGroup);
        }

        private void GameController_OnGameOver(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(GameOverBindingGroup);
        }

        private void RaisePropertyChangedForGroup(string GroupName)
        {
            string[] QuestionBindingProps = PropertyGroupAttribute.GetPropertyNamesByGroup(this.GetType(), GroupName);

            foreach (string Prop in QuestionBindingProps)
                RaisePropertyChanged(Prop);
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void Trivia_NewQuestion(object sender, EventArgs e)
        {
            TriviaContext.SelectedAnswer = null;
            RaisePropertyChangedForGroup(TriviaHudContext.QuestionBindingGroup);
        }

        public ICommand ReturnToMenuCommand
        {
            get;
            protected set;
        }

        public ICommand ShowHintsCommand
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

        [PropertyGroup(HintVisibilityGroup)]
        public Visibility HintFlyoutVisibility
        {
            get; set;
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

        public string[] UnlockedHints
        {
            get
            {
                return Trivia.Trivia.AvailableHints.ToArray();
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

        [PropertyGroup(GameOverBindingGroup)]
        public string GameOverMessage
        {
            get
            {
                if (!IsGameOver)
                    return null;

                switch(GameController.GameOverState.Cause)
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

        private void ReturnToMenu(object o)
        {
            SceneManager.LoadScene(SceneManager.MenuScene);
        }

        public void ShowHints(object o)
        {
            if(HintFlyoutVisibility == Visibility.Visible)
                HintFlyoutVisibility = Visibility.Hidden;
            else
                HintFlyoutVisibility = Visibility.Visible;

            RaisePropertyChangedForGroup(HintVisibilityGroup);
        }

        public void Update(GameTime GameTime)
        {
            TriviaContext.Update(GameTime);

            GameOverModalMarginAnimation.Update(GameTime, IsGameOver);
            if (MathHelper.Distance(GameOverModalMargin.Left, PreviousNotifiedGameOverMargin) > 0.01)
            {
                RaisePropertyChangedForGroup(GameOverVisibilityGroup);
                PreviousNotifiedGameOverMargin = GameOverModalMargin.Left;
            }


            GameOverModalOpacityAnimation.Update(GameTime, IsGameOver);
            if (MathHelper.Distance(GameOverModalOpacity, PreviousNotifiedGameOverOpacity) > 0.01)
            {
                RaisePropertyChangedForGroup(GameOverVisibilityGroup);
                PreviousNotifiedGameOverOpacity = GameOverModalOpacity;
                Log.Info("Opacity: " + GameOverModalOpacityAnimation.CurrentValue);
            }
        }
    }
}
