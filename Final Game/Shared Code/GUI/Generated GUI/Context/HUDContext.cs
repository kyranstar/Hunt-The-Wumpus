using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scores;
using System;
using EmptyKeys.UserInterface.Controls;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HUDContext : ViewModelBase
    {
        Player Player;
        GameController GameController;

        public HUDContext(GameController GameController)
        {
            this.GameController = GameController;
            this.Player = GameController.Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            GameController.NewQuestionHandler += Trivia_NewQuestion;

            SubmitAnswerCommand = new RelayCommand(new Action<object>(SubmitAnswer));
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void Trivia_NewQuestion(object sender, EventArgs e)
        {
            // TODO: Fix this bad code
            RaisePropertyChanged("CurrentTriviaQuestionAnswersAsComboBoxItems");
            RaisePropertyChanged("IsTriviaInProgress");
            RaisePropertyChanged("TriviaProgressAsVisibility");
            RaisePropertyChanged("CurrentTriviaQuestionText");
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

        // TODO: Use a converter instead of this
        public Visibility TriviaProgressAsVisibility
        {
            get
            {
                return IsTriviaInProgress ? Visibility.Visible : Visibility.Collapsed;
            }
        }

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
                if (GameController.CurrentTrivia == null)
                    return null;
                return GameController.CurrentTrivia.CurrentQuestion.AnswerChoices.ToArray();
            }
        }

        // TODO: AHHHHHHHHHHHHHHHHHHHHHHH
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

        public int SelectedAnswerIndex
        {
            get;
            set;
        }

        private void SubmitAnswer(object o)
        {
            GameController.CurrentTrivia.SubmitAnswer(this.CurrentTriviaQuestionAnswers[SelectedAnswerIndex]);
        }
    }
}
