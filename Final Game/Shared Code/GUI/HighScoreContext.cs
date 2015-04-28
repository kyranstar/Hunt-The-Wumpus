using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.Scores;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HighScoreContext : ViewModelBase
    {
        public HighScoreContext()
        {

        }

        public ScoreEntry[] HighScores
        {
            get
            {
                return new ScoreEntry[]
                {
                    new ScoreEntry() { Username = "Bob", ArrowsRemaining = 90}
                }; 
            }
        }
    }
}
