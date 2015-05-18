using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using System;

namespace HuntTheWumpus.SharedCode.GUI
{
    class GameOverContext : ViewModelBase
    {
        public GameOverContext()
        {
            MainMenuCommand = new RelayCommand(new Action<object>(LoadMenuScene));
            ShowScoresCommand = new RelayCommand(new Action<object>(LoadScoreScene));
        }

        public ICommand ShowScoresCommand
        {
            get;
            protected set;
        }

        public ICommand MainMenuCommand
        {
            get;
            protected set;
        }

        private void LoadMenuScene(object o)
        {
            SceneManager.LoadScene(SceneManager.MenuScene);
        }

        private void LoadScoreScene(object o)
        {
            SceneManager.LoadScene(SceneManager.HighScoreScene);
        }
    }
}
