using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GUI
{
    class MainMenuContext : ViewModelBase
    {
        public MainMenuContext()
        {
            RunGameCommand = new RelayCommand(new Action<object>(LoadGameScene));
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
    }
}
