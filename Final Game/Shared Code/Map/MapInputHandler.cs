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
    /// <summary>
    /// A class that handles inputs for the map.
    /// </summary>
    class MapInputHandler
    {
        private Map map;
        private Keys[] keys = {};

        public MapInputHandler(Map map)
        {
            this.map = map;
        }
        /// <summary>
        /// Handles all updates for the map this tick.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            foreach(Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (!keys.Contains(key))
                {
                    //new key is pressed
                    Log.Info("Key pressed: " + key);
                    HandleNewKeyPress(key);
                }
            }
            
            keys = Keyboard.GetState().GetPressedKeys();
        }

        private void HandleNewKeyPress(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    map.MovePlayer(Map.Direction.North);
                    break;
                case Keys.D:
                    map.MovePlayer(Map.Direction.East);
                    break;
                case Keys.A:
                    map.MovePlayer(Map.Direction.West);
                    break;
                case Keys.S:
                    map.MovePlayer(Map.Direction.South);
                    break;
            }
        }
    }
}
