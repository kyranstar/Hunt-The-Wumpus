using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GameControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GameMap
{
    class MapInputHandler
    {
        private Map map;
        private Keys[] keys = {};

        public MapInputHandler(Map map)
        {
            this.map = map;
        }

        public void Update(GameTime time)
        {
            foreach(Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (!keys.Contains(key))
                {
                    //new key is pressed
                    Log.Info("Key pressed: " + key);
                }
            }
            
            keys = Keyboard.GetState().GetPressedKeys();
        }
    }
}
