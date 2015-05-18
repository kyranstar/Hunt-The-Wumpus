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
        
        GameController GameController;

        private const string QuestionBindingGroup = "QuestionBinding";
        private const string QuestionVisibilityGroup = "QuestionVisibility";

        public HUDContext(GameController GameController)
        {
            this.GameController = GameController;
            this.Player = GameController.Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            GameController.NewQuestionHandler += Trivia_NewQuestion;

            SubmitAnswerCommand = new RelayCommand(new Action<object>(SubmitAnswer));

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
            SelectedAnswerIndex = 0;
            RaisePropertyChangedForGroup(QuestionBindingGroup);
        }

        public ICommand SubmitAnswerCommand
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

        [MemberGroup(QuestionVisibilityGroup)]
        [MemberGroup(QuestionBindingGroup)]
        public Visibility TriviaModalVisibility
        {
            get
            {
                return TriviaModalOpacity > 0.01 ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        
        [MemberGroup(QuestionVisibilityGroup)]
        public float TriviaModalOpacity
        {
            get
            {
                return MathHelper.Clamp(TriviaModalFadeAnimation.CurrentValue, 0, 1);
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

        private void SubmitAnswer(object o)
        {
            GameController.CurrentTrivia.SubmitAnswer(this.CurrentTriviaQuestionAnswers[SelectedAnswerIndex]);
        }

        public void Update(GameTime GameTime)
        {
            TriviaModalFadeAnimation.Update(GameTime, IsTriviaInProgress);

            if (MathHelper.Distance(TriviaModalOpacity, PreviousNotifiedTriviaOpacity) > 0.01)
            {
                RaisePropertyChangedForGroup(QuestionVisibilityGroup);
                PreviousNotifiedTriviaOpacity = TriviaModalOpacity;
            }
        }
    }
}
