using HuntTheWumpus.SharedCode.GameMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GUI
{
    class ScoreHudContext
    {
        private Player Player;

        public ScoreHudContext(Player player)
        {
            Player = player;
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

        public void Reset()
        {
            Player.Reset();
        }
    }
}
