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

namespace HuntTheWumpus.SharedCode.GUI
{
    class TriviaHudContext
    {
        public const string QuestionBindingGroup = "QuestionBinding";
        public const string QuestionVisibilityGroup = "QuestionVisibility";

        private GameController GameController;

        private StateAnimator TriviaModalFadeAnimation;
        private float PreviousNotifiedTriviaOpacity = 0;

        Action<string> RaisePropertyChangedForGroup;

        public TriviaHudContext(GameController gameController, Action<string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            GameController = gameController;
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

        private void SubmitAnswer(object o)
        {
            if (SelectedAnswerIndex >= 0)
                GameController.CurrentTrivia.SubmitAnswer(CurrentTriviaQuestionAnswers[SelectedAnswerIndex]);
            else
                // TODO: Present a message here
                return;
        }

        public void Reset()
        {
            TriviaModalFadeAnimation.Reset();
            PreviousNotifiedTriviaOpacity = 0;
        }

        public bool IsTriviaInProgress
        {
            get
            {
                return GameController.CurrentTrivia != null && GameController.CurrentTrivia.CurrentQuestion != null;
            }
        }

        [PropertyGroup(QuestionVisibilityGroup)]
        [PropertyGroup(QuestionBindingGroup)]
        public Visibility TriviaModalVisibility
        {
            get
            {
                return TriviaModalOpacity > 0.01 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [PropertyGroup(QuestionVisibilityGroup)]
        public float TriviaModalOpacity
        {
            get
            {
                return MathHelper.Clamp(TriviaModalFadeAnimation.CurrentValue, 0, 1);
            }
        }

        [PropertyGroup(QuestionBindingGroup)]
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

        [PropertyGroup(QuestionBindingGroup)]
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

        [PropertyGroup(QuestionBindingGroup)]
        public int SelectedAnswerIndex
        {
            get;
            set;
        }

        [PropertyGroup(QuestionBindingGroup)]
        public ComboBoxItem SelectedAnswer
        {
            get;
            set;
        }

        [PropertyGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsCorrect
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.NumberCorrect;
            }
        }

        [PropertyGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsCompleted
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.QuestionCounter;
            }
        }

        [PropertyGroup(QuestionBindingGroup)]
        public int NumTriviaQuestionsTotal
        {
            get
            {
                if (GameController.CurrentTrivia == null)
                    return -1;

                return GameController.CurrentTrivia.QList.Count;
            }
        }

        public ICommand SubmitAnswerCommand
        {
            get;
            protected set;
        }

        public void Update(GameTime gameTime)
        {
            TriviaModalFadeAnimation.Update(gameTime, IsTriviaInProgress);
            if (MathHelper.Distance(TriviaModalOpacity, PreviousNotifiedTriviaOpacity) > 0.01)
            {
                RaisePropertyChangedForGroup(QuestionVisibilityGroup);
                PreviousNotifiedTriviaOpacity = TriviaModalOpacity;
            }
        }
    }
}
