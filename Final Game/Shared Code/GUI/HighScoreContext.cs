using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.Scores;
using System;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HighScoreContext : ViewModelBase
    {
        ScoreManager ScoreMan;
        public HighScoreContext()
        {
            ScoreMan = new ScoreManager();
            ScoreMan.Load();

            // TODO: REMOVE THIS
            if (ScoreMan.Scores.Length <= 0)
            {
                ScoreMan.AddScore(new ScoreEntry()
                {
                    ArrowsRemaining = 90,
                    GoldRemaining = 83,
                    TurnsTaken = 5,
                    LocalScoreDate = DateTime.Now,
                    Username = "Fred"
                });
                ScoreMan.Save();
            }
        }

        public ScoreEntry[] HighScores
        {
            get
            {
                return ScoreMan.Scores;
            }
        }
    }
}
