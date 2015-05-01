using HuntTheWumpus.SharedCode.GameControl;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        private List<Room> TraveledPath = new List<Room>();

        public int[] PlayerPath
        {
            get
            {
                return TraveledPath.Select(r => r.RoomID).ToArray();
            }
        }

        /// <summary>
        /// Holds the id of the room that the player is currently in.
        /// </summary>
        public int PlayerRoom
        {
            get;
            protected set;
        }
        /// <summary>
        /// Holds the location of the player within the current room. The origin is the center of this room.
        /// </summary>
        public Point PlayerLocation;

        private Cave _cave;
        /// <summary>
        /// Stores the cave, which stores all the room data
        /// </summary>
        public Cave Cave
        {
            get { return _cave; }
            set
            {
                if (value.IsValid)
                    _cave = value;
                else
                    throw new ArgumentException("The given cave was invalid.");
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
            Log.Info("Creating map...");
            Cave = MapGenerator.GenerateRandomCave();
            Wumpus = new Wumpus(Cave);
            Player = new Player();
            PlayerLocation = new Point(0, 0);

            RoomUpdate();
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
            if (currentRoom.AdjacentRooms[dir] != -1)
            {
                //set our current room to that room
                PlayerRoom = Cave.GetRoom(currentRoom.AdjacentRooms[(int)dir]).RoomID;
                RoomUpdate();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Call this method whenever entering a new room.
        /// </summary>
        private void RoomUpdate()
        {

            CollectItemsFromRoom();
            TraveledPath.Add(Cave[PlayerRoom]);

            if (Wumpus.Location == PlayerRoom)
            {
                // We're in the same room as the wumpus!

                // We need to ask the player 5 trivia questions.
                int triviaQuestionsRight = 5;
                const int NUM_TO_BEAT_WUMPUS = 3;
                if (triviaQuestionsRight < NUM_TO_BEAT_WUMPUS)
                {
                    //TODO: Game over.
                }
                else
                {
                    // Move the wumpus 2-4 rooms away.
                    int oldLocation = Wumpus.Location;

                    List<int> validRooms = new List<int>();
                    Random r = new Random();
                    foreach (int i in Enumerable.Range(0, Cave.RoomDict.Count).OrderBy(x => r.Next()))
                    {
                        KeyValuePair<int, Room> pair = Cave.RoomDict.ElementAt(i);

                        if (pair.Value.HasBats || pair.Value.HasPit) continue;

                        int? distance = Cave.Distance(pair.Value, Cave[PlayerRoom], true);
                        if (distance.HasValue && distance.Value >= 2 && distance.Value <= 4)
                        {
                            Wumpus.Location = pair.Key;
                        }
                    }

                    Debug.Assert(oldLocation != Wumpus.Location);
                    Log.Info("You scared the Wumpus away to room " + Wumpus.Location);
                }

            }

        }
        /// <summary>
        /// The player collects items from his current room. Call this when the player enters a new room.
        /// </summary>
        private void CollectItemsFromRoom()
        {
            Room currentRoom = Cave.GetRoom(PlayerRoom);
            Player.Gold += currentRoom.Gold;
            Player.Arrows += currentRoom.Arrows;

            currentRoom.Gold = currentRoom.Arrows = 0;
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
        /// Moves a player to the specified room
        /// </summary>
        /// <param name="RoomID"></param>
        /// <returns>Whether the move was valid.</returns>
        public bool MovePlayerTo(int RoomID)
        {
            if (Cave.GetRoom(RoomID) == null)
                return false;

            PlayerRoom = RoomID;
            RoomUpdate();
            return true;
        }

        /// <summary>
        /// Takes a room ID and determines whether the player can shoot to that room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Whether the player can shoot into the given room (if the player is adjacent to the room).</returns>
        public bool CanShoot(int roomId)
        {
            return Cave.GetRoom(PlayerRoom).AdjacentRooms.Contains(roomId);
        }

        /// <summary>
        /// Shoots an arrow into the room with the given id. If you hit the wumpus, you win. If you run out of arrows, you lose.
        /// </summary>
        /// <param name="roomId">The room to shoot into</param>
        public void ShootArrow(int roomId)
        {
            if (!CanShoot(roomId))
            {
                throw new Exception("Always check if you can shoot arrows before you shoot!");
            }
            Player.Arrows--;

            if (Wumpus.Location == roomId)
            {
                //TODO: You shot the wumpus. You win? Or trivia?
            }
            else if (Player.Arrows <= 0)
            {
                //TODO: Game over! 
            }

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

            int[] adjacentRooms = Cave.GetRoom(PlayerRoom).AdjacentRooms;
            foreach (int room in adjacentRooms)
            {
                Room r = Cave.GetRoom(room);
                if (r.HasBats) list.Add(PlayerWarnings.Bat);
                if (r.HasPit) list.Add(PlayerWarnings.Pit);
            }
            if (adjacentRooms.Contains(Wumpus.Location)) list.Add(PlayerWarnings.Wumpus);

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
            Wumpus,
        }
        /// <summary>
        /// Returns a string description of a warning
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static string GetWarningDescription(PlayerWarnings warning)
        {
            switch (warning)
            {
                case PlayerWarnings.Pit: return "I feel a draft.";
                case PlayerWarnings.Bat: return "Bats nearby.";
                case PlayerWarnings.Wumpus: return "I smell a Wumpus!";

                default: throw new Exception();
            }
        }

        /// <summary>
        /// An enumeration of hexagonal directions
        /// </summary>
        public enum Direction
        {
            North,
            Northeast,
            Southeast,
            South,
            Southwest,
            Northwest,
        }

        /// <summary>
        /// An enumeration of square directions
        /// </summary>
        public enum SquareDirection
        {
            North,
            East,
            South,
            West,
        }
    }
}
