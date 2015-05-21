using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Scores;
using System;

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

        public ScoreEntry[] HighScores
        {
            get
            {
                return ScoreMan.Scores;
            }
        }

        private void LoadMenuScene(object o)
        {
            SceneManager.LoadScene(SceneManager.MenuScene);
        }
    }
}
