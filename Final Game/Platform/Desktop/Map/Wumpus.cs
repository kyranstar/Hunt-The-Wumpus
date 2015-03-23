using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Platform.Desktop.Map
{
    class Wumpus
    {
        private int x, y;
        private Cave cave;

        public Wumpus(Cave cave)
        {
            this.cave = cave;
        }
        /// <summary>
        /// Moves the wumpus to a nearby valid position
        /// </summary>
        public void Move()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The position of the wumpus</returns>
        public Point GetPosition()
        {
            return new Point(x, y);
        }
    }
}
