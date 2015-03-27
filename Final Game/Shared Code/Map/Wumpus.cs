using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.Map
{
    class Wumpus
    {
        private Point location;
        public Point Location
        {
            get { return location; }
        }
        private Cave.Cave cave;

        public Wumpus(Cave.Cave cave)
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
