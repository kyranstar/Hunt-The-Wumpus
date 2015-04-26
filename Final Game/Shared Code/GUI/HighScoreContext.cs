using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Scores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
