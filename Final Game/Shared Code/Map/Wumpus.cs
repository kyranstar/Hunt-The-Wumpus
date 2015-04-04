using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// This class represents the wumpus in a game.
    /// </summary>
    public class Wumpus
    {
        /// <summary>
        /// The current location of the wumpus
        /// </summary>
        public int Location
        {
            set; get;
        }
        private Cave cave;

        /// <summary>
        /// Initializes a new instance of the Wumpus class.
        /// </summary>
        /// <param name="cave"></param>
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
