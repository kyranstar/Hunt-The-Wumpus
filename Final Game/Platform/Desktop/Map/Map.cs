using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Platform.Desktop.Map
{
    /// <summary>
    /// The map object tracks the locations of all of the objects in the current game. The tasks it performs are as follows:
    /// <list type="bullet">
    /// <item> 
    /// <description>Store and interact with the cave used for this game.</description> 
    /// </item> 
    /// <item> 
    /// <description>Keep track of where the hazards are.</description> 
    /// </item> 
    /// <item> 
    /// <description>Control arrow shooting.</description> 
    /// </item> 
    /// <item> 
    /// <description>Give player warnings.</description> 
    /// </item> 
    /// <item> 
    /// <description>Obtain secrets to help the player.</description> 
    /// </item> 
    /// </list> 
    /// </summary>
    class Map
    {
        private int playerX, playerY;
        private Cave cave;
        private Wumpus wumpus;

        public Map()
        {
            cave = new Cave();
            wumpus = new Wumpus(cave);
        }

        /// <summary>
        /// This moves the wumpus to a new position.
        /// </summary>
        public void MoveWumpus()
        {
            wumpus.Move();
        }
        /// <summary>
        /// Moves the player, if possible, to the new point. The point is calculated by the current player position + p. 
        /// </summary>
        /// <param name="p">P is relative to the current player's position</param>
        public void MovePlayer(Point p)
        {

        }

        /// <summary>
        ///  Takes a point and determines whether the player can shoot to that point.
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Whether the player can shoot to point p</returns>
        public bool CanShoot(Point p)
        {
            return false;
        }

        /// <summary>
        /// Gives all warnings that the player should know about. This includes if the player is one (accessable) tile away from:
        /// <list type="bullet">
        /// <item> 
        /// <description>a pit</description> 
        /// </item> 
        /// <item> 
        /// <description>a bat</description> 
        /// </item> 
        /// <item> 
        /// <description>The wumpus</description> 
        /// </item> 
        /// </list> 
        /// </summary>
        /// <returns>a list of warnings</returns>
        public List<String> PlayerWarnings()
        {
            return new List<String>();
        }
    }
}
