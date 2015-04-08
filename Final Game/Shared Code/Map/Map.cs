using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GameControl;

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

        private Cave _cave;
        /// <summary>
        /// Stores the cave, which stores all the room data
        /// </summary>
        public Cave Cave {
            get {return _cave;} 
            set {
                AssertCorrectLayout(value);
                _cave = value;
            }
        }
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
            AssertCorrectLayout(Cave);
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
        /// <param name="dir"></param>
        public bool MovePlayer(int dir)
        {
            Room currentRoom = Cave.GetRoom(PlayerRoom);
            //if the room in the direction exists
            if (currentRoom.adjacentRooms[dir] != -1)
            {
                //set our current room to that room
                PlayerRoom = Cave.GetRoom(currentRoom.adjacentRooms[(int)dir]).roomId;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(Direction dir)
        {
            return MovePlayer((int)dir);
        }

        /// <summary>
        /// Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(SquareDirection dir)
        {
            return MovePlayer((int)dir);
        }

        /// <summary>
        /// Moves the player to the specified room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayerTo(int RoomID)
        {
            if (Cave.GetRoom(RoomID) == null)
                return false;

            PlayerRoom = RoomID;
            return true;
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
        /// This method asserts that this cave is valid. Don't call this in production code probably
        /// </summary>
        /// <param name="theCave"></param>
        public void AssertCorrectLayout(Cave theCave)
        {
            Dictionary<int, Room> cave = theCave.getRoomDict();
            //Checks that all rooms only connect to rooms that exist or to -1
            bool allConnectionsValid = cave.Values.All<Room>(
                //For each room make sure
                   (e) => e.adjacentRooms.All(
                       // For each connection make sure it exists or its a null connection
                       (r) =>  cave.Keys.Contains(r) || r == -1
                       
                   ));
            bool allRoomsCanReachEachOther = true;
            //cave.Values.All(
            //    (i) => cave.Values.All(
            //        (j) => i.roomId == j.roomId || i.CanAccess(j) 
            //        )
            //    );

            if (!allConnectionsValid)
            {
               Log.Error("A room refers to a room that does not exist!");
            }
            if (!allRoomsCanReachEachOther)
            {
                Log.Error("At least one room can not reach another!");
            }
            
            Log.Info("Cave layout is valid!");
            
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
        /// An enumeration of hexagonal directions
        /// </summary>
        public enum Direction
        {
            //Hex directions
            North,
            Northeast,
            Southeast,
            South,
            Southwest,
            Northwest

        }

        /// <summary>
        /// An enumeration of square directions
        /// </summary>
        public enum SquareDirection
        {
            //Hex directions
            North,
            East,
            South,
            West

        }
    }
}
