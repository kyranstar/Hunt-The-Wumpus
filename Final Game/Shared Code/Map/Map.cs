using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode
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
        // Holds the id of the room that the player is currently in.
        private int playerRoom;
        public int PlayerRoom
        {
            get { return playerRoom; }
            protected set { playerRoom = value; }
        }

        public readonly Cave Cave;
        private Wumpus wumpus;

        public Map()
        {
            GameControl.Log.Info("Creating map...");
            Cave = new Cave();
            wumpus = new Wumpus(Cave);

            new MapGenerator().generateMap(this);
        }

        /// <summary>
        /// This moves the wumpus to a new position.
        /// </summary>
        public void MoveWumpus()
        {
            wumpus.Move();
        }
        /// <summary>
        /// Moves the player to the new room if the room is adjacent to this room.
        /// </summary>
        /// <param name="roomId"></param>
        public void MovePlayer(int roomId)
        {

        }

      /// <summary>
       /// Takes a room ID and determines whether the player can shoot to that room.
      /// </summary>
      /// <param name="roomId"></param>
      /// <returns></returns>
        public bool CanShoot(int roomId)
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
        public List<PlayerWarnings> GetPlayerWarnings()
        {
            List<PlayerWarnings> list = new List<PlayerWarnings>();
            
            return list;
        }
        public enum PlayerWarnings
        {
            Pit,
            Bat,
            Wumpus
        }
    }
}
