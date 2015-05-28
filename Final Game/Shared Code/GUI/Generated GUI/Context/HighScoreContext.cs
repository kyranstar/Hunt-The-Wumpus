using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.Scores;
using System;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HighScoreContext : ViewModelBase
    {
        ScoreManager ScoreMan;
        public HighScoreContext()
        {
            BackCommand = new RelayCommand(new Action<object>(LoadMenuScene));

            ScoreMan = new ScoreManager();
            ScoreMan.Load();
        }
        
        public ICommand BackCommand
        {
            get;
            protected set;
        }

        public RenderableScoreEntry[] HighScores
        {
            get
            {
                return ScoreMan.Scores.Select(s => new RenderableScoreEntry()
                    {
                        Date = s.LocalScoreDate.ToShortDateString(),
                        Score = s.Score,
                        // Limit name length
                        Username = s.Username.Truncate(10)
                    }).ToArray();
            }
        }

        private void LoadMenuScene(object o)
        {
            SceneManager.LoadScene(SceneManager.MenuScene);
        }
    }

    public class RenderableScoreEntry
    {
        public int Score { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
    }
}
