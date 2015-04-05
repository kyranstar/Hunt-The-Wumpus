using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.GameMap
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
    public class Map
    {
        private int playerRoom;
        /// <summary>
        /// Holds the id of the room that the player is currently in.
        /// </summary>
        public int PlayerRoom
        {
            get { return playerRoom; }
            protected set { playerRoom = value; }
        }

        /// <summary>
        /// Stores the cave, which stores all the room data
        /// </summary>
        public readonly Cave Cave;
        /// <summary>
        /// Holds a reference to the wumpus
        /// </summary>
        public readonly Wumpus Wumpus;
        /// <summary>
        /// Holds a reference to the current player
        /// </summary>
        public readonly Player Player;

        /// <summary>
        /// Constructs the map and generates the cave with a MapGenerator.
        /// </summary>
        public Map()
        {
            GameControl.Log.Info("Creating map...");
            Cave = new Cave();
            Wumpus = new Wumpus(Cave);
            Player = new Player();

            new MapGenerator().generateMap(this);
        }

        /// <summary>
        /// This moves the wumpus to a new position.
        /// </summary>
        public void MoveWumpus()
        {
            Wumpus.Move();
        }
        /// <summary>
        /// Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="roomId"></param>
        public void MovePlayer(Direction dir)
        {
            Room currentRoom = Cave.getRoom(PlayerRoom);
            //if the room in the direction exists
            if (currentRoom.adjacentRooms[(int)dir] != -1)
            {
                //set our current room to that room
                PlayerRoom = Cave.getRoom(currentRoom.adjacentRooms[(int)dir]).roomId;
            }
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
        /// <summary>
        /// Represents warnings to be given to the player if they are too close to something.
        /// </summary>
        public enum PlayerWarnings
        {
            /// <summary>
            /// Represents when the player is within one tile of a pit
            /// </summary>
            Pit,
            /// <summary>
            /// Represents when the player is within one tile of a bat
            /// </summary>
            Bat,
            /// <summary>
            /// Represents when the player is within one (maybe more for the wumpus? we should discuss this) tile of the wumpus
            /// </summary>
            Wumpus
        }
        /// <summary>
        /// An enumeration of directions
        /// </summary>
        public enum Direction
        {
            //Hex directions
            /*Up,
            UpRight,
            DownRight,
            Down,
            DownLeft,
            UpLeft
             * */
            //Right now we're using square directions
            //For some reason we need to swap around South and North for it to work properly?
            South,
            East,
            North,
            West
        }
    }
}
