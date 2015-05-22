using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.Scores;
using System.ComponentModel;
using System;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// Represents the player. Holds his current stats like gold, arrows, etc.
    /// </summary>
    public class Player : INotifyPropertyChanged
    {
        private ScoreEntry InternalScore;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string PropName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropName));
        }

        public ScoreEntry ScoreEntry
        {
            get
            {
                return InternalScore;
            }
        }

        public void Reset()
        {
            InternalScore = new ScoreEntry();
        }


        //Required fields and their getters
        public int Gold 
        {
            get { return InternalScore.GoldRemaining; }
            set
            {
                InternalScore.GoldRemaining = value;
                RaisePropertyChanged("Gold");
            }
        }
        
        public int Arrows
        {
            get { return InternalScore.ArrowsRemaining; }
            set
            {
                InternalScore.ArrowsRemaining = value;
                RaisePropertyChanged("Arrows");
            }
        }

        public int Turns
        {
            get { return InternalScore.TurnsTaken; }
            set
            {
                InternalScore.TurnsTaken = value;
                RaisePropertyChanged("Turns");
            }
        }

        public int Score
        {
            get
            {
                return InternalScore.Score;
            }
        }

        public Player()
        {
            // TODO: Add default score params
            InternalScore = new ScoreEntry();
        }
    }
}
