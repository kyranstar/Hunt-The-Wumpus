using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scores;
using System;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HUDContext : ViewModelBase
    {
        Player Player;
        public HUDContext(Player Player)
        {
            this.Player = Player;
            Player.PropertyChanged += Player_PropertyChanged;
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(e.PropertyName);
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
    }
}
