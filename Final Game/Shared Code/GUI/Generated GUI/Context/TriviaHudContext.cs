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
        
        [PropertyGroup(XamlBoundAnimation.XamlAnimationGroupName, QuestionVisibilityGroup)]
        public XamlBoundOpacityAnimation TriviaModalFadeAnimation { get; protected set; }

        Action<string, string> RaisePropertyChangedForGroup;

        public TriviaHudContext(GameController gameController, Action<string, string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            GameController = gameController;
            SubmitAnswerCommand = new RelayCommand(new Action<object>(SubmitAnswer));

            TriviaModalFadeAnimation = new XamlBoundOpacityAnimation(
                new XamlAnimationDescriptor
                {
                     MinValue = 0,
                     MaxValue = 1,
                     SecondaryGroupName = QuestionVisibilityGroup
                }, RaisePropertyChangedForGroup);
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
        }

        public bool IsTriviaInProgress
        {
            get
            {
                return GameController.CurrentTrivia != null && GameController.CurrentTrivia.CurrentQuestion != null;
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
        }
    }
}
