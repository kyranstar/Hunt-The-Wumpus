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

        private StateAnimator TriviaModalFadeAnimation;
        private float PreviousNotifiedTriviaOpacity = 0;

        private StateAnimator GameOverModalMarginAnimation;
        private float PreviousNotifiedGameOverMargin = 0;

        private StateAnimator GameOverModalOpacityAnimation;
        private float PreviousNotifiedGameOverOpacity = 0;

        GameController GameController;

        private const string QuestionBindingGroup = "QuestionBinding";
        private const string QuestionVisibilityGroup = "QuestionVisibility";
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

            SubmitAnswerCommand = new RelayCommand(new Action<object>(SubmitAnswer));
            ReturnToMenuCommand = new RelayCommand(new Action<object>(ReturnToMenu));
            ShowHintsCommand = new RelayCommand(new Action<object>(ShowHints));

            HintFlyoutVisibility = Visibility.Hidden;

            TriviaModalFadeAnimation = new StateAnimator(
                Pct =>
                    {
                        // TODO: Add math to do cubic Bezier curve:
                        // (0.165, 0.84), (0.44, 1)
                        return (float)Math.Pow(Pct, 2);
                    },
                Pct =>
                    {
                        return (float)-Math.Pow(Pct, 2) + 1;
                    },
                1);


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

        private void GameController_OnGameOver(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(GameOverBindingGroup);
        }

        private void RaisePropertyChangedForGroup(string GroupName)
        {
            string[] QuestionBindingProps = MemberGroupAttribute.GetMemberNamesByGroup(this, GroupName);

            foreach (string Prop in QuestionBindingProps)
                RaisePropertyChanged(Prop);
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void Trivia_NewQuestion(object sender, EventArgs e)
        {
            SelectedAnswer = null;
            RaisePropertyChangedForGroup(QuestionBindingGroup);
        }

        public ICommand SubmitAnswerCommand
        {
            get;
            protected set;
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

        public int Gold
        {
            get
            {
                return Player.Gold;
            }
        }

        public int Arrows
        {
            get
            {
                return Player.Arrows;
            }
        }
        
        public int Turns
        {
            get
            {
                return Player.Turns;
            }
        }

        public bool IsTriviaInProgress
        {
            get
            {
                return GameController.CurrentTrivia != null && GameController.CurrentTrivia.CurrentQuestion != null;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return GameController.GameOverState != null;
            }
        }

        [MemberGroup(QuestionVisibilityGroup)]
        [MemberGroup(QuestionBindingGroup)]
        public Visibility TriviaModalVisibility
        {
            get
            {
                return TriviaModalOpacity > 0.01 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [MemberGroup(GameOverVisibilityGroup)]
        public Visibility GameOverModalVisibility
        {
            get
            {
                return GameOverModalOpacity > 0.01 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [MemberGroup(HintVisibilityGroup)]
        public Visibility HintFlyoutVisibility
        {
            get; set;
        }

        [MemberGroup(QuestionVisibilityGroup)]
        public float TriviaModalOpacity
        {
            get
            {
                return MathHelper.Clamp(TriviaModalFadeAnimation.CurrentValue, 0, 1);
            }
        }

        [MemberGroup(GameOverVisibilityGroup)]
        public float GameOverModalOpacity
        {
            get
            {
                return MathHelper.Clamp(GameOverModalOpacityAnimation.CurrentValue, 0, 1);
            }
        }

        [MemberGroup(GameOverVisibilityGroup)]
        public Thickness GameOverModalMargin
        {
            get
            {
                return new Thickness(GameOverModalMarginAnimation.CurrentValue, 0, -GameOverModalMarginAnimation.CurrentValue, 0);
            }
        }

        [MemberGroup(QuestionBindingGroup)]
        public string CurrentTriviaQuestionText
        {
            get
            {
                if (!IsTriviaInProgress)
                    return null;
                return GameController.CurrentTrivia.CurrentQuestion.QuestionText;
            }
        }

        public string[] CurrentTriviaQuestionAnswers
        {
            get
            {
                if (!IsTriviaInProgress)
                    return null;
                return GameController.CurrentTrivia.CurrentQuestion.AnswerChoices.ToArray();
            }
        }

        [MemberGroup(QuestionBindingGroup)]
        public ComboBoxItem[] CurrentTriviaQuestionAnswersAsComboBoxItems
        {
            get
            {
                if (CurrentTriviaQuestionAnswers == null)
                    return new ComboBoxItem[] { new ComboBoxItem() { Content = "<no trivia answers available>" } };
                return CurrentTriviaQuestionAnswers.Select(x => new ComboBoxItem()
                {
                    Content = x
                }).ToArray();
            }
        }

        public string[] UnlockedHints
        {
            get
            {
                return Trivia.Trivia.AvailableHints.ToArray();
            }
        }

        [MemberGroup(QuestionBindingGroup)]
        public int SelectedAnswerIndex
        {
            get;
            set;
        }

        [MemberGroup(QuestionBindingGroup)]
        public ComboBoxItem SelectedAnswer
        {
            get;
            set;
        }

        [MemberGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsCorrect
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.NumberCorrect;
            }
        }

        [MemberGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsCompleted
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.QuestionCounter;
            }
        }

        [MemberGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsTotal
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.QList.Count;
            }
        }

        [MemberGroup(GameOverBindingGroup)]
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

        private void SubmitAnswer(object o)
        {
            if (SelectedAnswerIndex >= 0)
                GameController.CurrentTrivia.SubmitAnswer(CurrentTriviaQuestionAnswers[SelectedAnswerIndex]);
            else
                // TODO: Present a message here
                return;
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
            TriviaModalFadeAnimation.Update(GameTime, IsTriviaInProgress);
            if (MathHelper.Distance(TriviaModalOpacity, PreviousNotifiedTriviaOpacity) > 0.01)
            {
                RaisePropertyChangedForGroup(QuestionVisibilityGroup);
                PreviousNotifiedTriviaOpacity = TriviaModalOpacity;
            }


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
