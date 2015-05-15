using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scores;
using System;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HUDContext : ViewModelBase
    {
        Player Player;
        Map Map;
        public HUDContext(Map Map)
        {
            this.Map = Map;
            this.Player = Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            Map.NewQuestionHandler += Trivia_NewQuestion;
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void Trivia_NewQuestion(object sender, EventArgs e)
        {
            // TODO: Fix this bad code
            RaisePropertyChanged("IsTriviaInProgress");
            RaisePropertyChanged("TriviaProgressAsVisibility");
            RaisePropertyChanged("CurrentTriviaQuestion");
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
                return Map.CurrentTrivia != null;
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
                if (Map.CurrentTrivia == null)
                    return null;
                return Map.CurrentTrivia.CurrentQuestion.QuestionText;
            }
        }
    }
}
