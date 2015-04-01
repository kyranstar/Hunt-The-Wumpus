using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode
{
    class Wumpus
    {
        private int location;
        public int Location
        {
            get { return location; }
        }
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
    }
}
