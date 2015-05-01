using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using System;

namespace HuntTheWumpus.SharedCode.GUI
{
    class MainMenuContext : ViewModelBase
    {
        public MainMenuContext()
        {
            RunGameCommand = new RelayCommand(new Action<object>(LoadGameScene));
            ShowScoresCommand = new RelayCommand(new Action<object>(LoadScoreScene));
        }

        public ICommand ShowScoresCommand
        {
            get;
            protected set;
        }

        public ICommand RunGameCommand
        {
            get;
            protected set;
        }

        private void LoadGameScene(object o)
        {
            SceneManager.LoadScene(SceneManager.GameScene);
        }

        private void LoadScoreScene(object o)
        {
            SceneManager.LoadScene(SceneManager.HighScoreScene);
        }
    }
}
