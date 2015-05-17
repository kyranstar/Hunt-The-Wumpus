using System;
using System.Collections.Generic;
using System.Linq;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Trivia;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    ///     The map object tracks the locations of all of the objects in the current game. The tasks it performs are as
    ///     follows:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>Store and interact with the cave used for this game.</description>
    ///         </item>
    ///         <item>
    ///             <description>Keep track of where the hazards are.</description>
    ///         </item>
    ///         <item>
    ///             <description>Control arrow shooting.</description>
    ///         </item>
    ///         <item>
    ///             <description>Give player warnings.</description>
    ///         </item>
    ///         <item>
    ///             <description>Obtain secrets to help the player.</description>
    ///         </item>
    ///     </list>
    /// </summary>
    public class Map
    {
        // TODO: Send new room info in EventArgs
        public delegate void PlayerMoveHandler(object sender, EventArgs e);
        public event PlayerMoveHandler OnPlayerMoved;

        /// <summary>
        ///     An enumeration of hexagonal directions
        /// </summary>
        public enum Direction
        {
            North,
            Northeast,
            Southeast,
            South,
            Southwest,
            Northwest
        }

        /// <summary>
        ///     Represents warnings to be given to the player if they are too close to something.
        /// </summary>
        public enum PlayerWarnings
        {
            /// <summary>
            ///     Represents when the player is within one tile of a pit
            /// </summary>
            Pit,

            /// <summary>
            ///     Represents when the player is within one tile of a bat
            /// </summary>
            Bat,

            /// <summary>
            ///     Represents when the player is within one (maybe more for the wumpus? we should discuss this) tile of the wumpus
            /// </summary>
            Wumpus
        }

        /// <summary>
        ///     An enumeration of square directions
        /// </summary>
        public enum SquareDirection
        {
            North,
            East,
            South,
            West
        }

        /// <summary>
        ///     Holds a reference to the current player
        /// </summary>
        public readonly Player Player;

        /// <summary>
        ///     Holds a reference to the wumpus
        /// </summary>
        public readonly Wumpus Wumpus;

        private Cave _cave;
        public MapInputHandler InputHandler;
        public int MoveCount;

        /// <summary>
        ///     Holds the location of the player within the current room. The origin is the center of this room.
        /// </summary>
        public Point PlayerLocation;

        public ISet<int> PlayerPath = new HashSet<int>();

        /// <summary>
        ///     Constructs the map and generates the cave with a MapGenerator.
        /// </summary>
        public Map()
        {
            Log.Info("Creating map...");
            Cave = MapGenerator.GenerateRandomCave();
            Wumpus = new Wumpus(this);
            Player = new Player();

            InputHandler = new MapInputHandler(this);

            PlayerLocation = new Point(0, 0);

            Wumpus.MoveToRandomRoom();
            CollectItemsFromRoom();
            PlayerPath.Add(PlayerRoom);
        }

        /// <summary>
        ///     Holds the id of the room that the player is currently in.
        /// </summary>
        public int PlayerRoom { get; protected set; }

        /// <summary>
        ///     Stores the cave, which stores all the room data
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

        public bool TryShootTowards(Direction direction)
        {
            // TODO: Animate shooting

            Player.Arrows--;

            int targetRoom = Cave[PlayerRoom].AdjacentRooms[(int) direction];
            if (CanShootTo(targetRoom))
            {
                if (Wumpus.Location == targetRoom)
                {
                    // TODO: end game
                    Log.Info("Yay! You shot the wumpus!");
                    return true;
                }
                // TODO: Present message (miss)
                Log.Info("Your arrow missed the wumpus.");
                return false;
            }
            // TODO: Present message (hit wall)
            Log.Info("You managed to shoot a wall. Good job.");
            return false;
        }

        public void Update(GameTime gameTime)
        {
            InputHandler.Update(gameTime);
        }

        /// <summary>
        ///     Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(int dir)
        {
            Room currentRoom = Cave.GetRoom(PlayerRoom);
            //if the room in the direction exists
            if (currentRoom.AdjacentRooms[dir] != -1)
            {
                //set our current room to that room
                PlayerRoom = Cave.GetRoom(currentRoom.AdjacentRooms[dir]).RoomID;

                // Update player tracking
                ProcessPlayerMove();

                //Wumpus has to move after the player does
                Wumpus.Move();

                return true;
            }
            Wumpus.Move();
            return false;
        }

        /// <summary>
        ///     Call this method whenever entering a new room.
        /// </summary>
        private void ProcessPlayerMove()
        {
            CollectItemsFromRoom();
            PlayerPath.Add(PlayerRoom);
            MoveCount++;
            Player.Turns = MoveCount;

            Room currentRoom = Cave[PlayerRoom];
            if (currentRoom.HasBats)
            {
                Log.Info("Player hit bats and was moved!");
                // Move player to random room without a hazard

                Random rand = new Random();
                var nonHazardousRooms = Cave.Rooms.
                    OrderBy(e => rand.Next()).
                    Where(r => !r.HasPit && !r.HasBats && Wumpus.Location != r.RoomID).ToList();

                PlayerRoom = nonHazardousRooms.First().RoomID;
                // Move bats to another random room without hazard and without player
                currentRoom.HasBats = false;
                nonHazardousRooms.First(r => r.RoomID != PlayerRoom).HasBats = true;

                ProcessPlayerMove();
            }
            else if (currentRoom.HasPit)
            {
                const int numToAsk = 3;

                // Ask 3 questions
                // If 2 or more are right
                int numCorrect = 2;
                if (numCorrect >= 2)
                {
                    // Place player in already visited location without hazards
                    Room alreadyVisited = PlayerPath.Select(i => Cave[i]).
                        FirstOrDefault(
                            r => !r.HasBats && !r.HasPit && Wumpus.Location != r.RoomID && PlayerRoom != r.RoomID);
                    // If there are no non-hazardous locations weve already visited
                    if (alreadyVisited == null)
                    {
                        // Check all possible rooms instead
                        var allRooms = Cave.Rooms.
                            FirstOrDefault(
                                r =>
                                    !r.HasBats && !r.HasPit && Wumpus.Location != r.RoomID && PlayerRoom != r.RoomID);
                        if (allRooms != null)
                        {
                            PlayerRoom = allRooms.RoomID;
                        }
                    }
                    else
                    {
                        PlayerRoom = alreadyVisited.RoomID;
                    }
                }

                ProcessPlayerMove();
            }

            OnPlayerMoved(this, new EventArgs());
        }

        /// <summary>
        ///     The player collects items from his current room. Call this when the player enters a new room.
        /// </summary>
        private void CollectItemsFromRoom()
        {
            Room currentRoom = Cave.GetRoom(PlayerRoom);
            Player.Gold += currentRoom.Gold;
            Player.Arrows += currentRoom.Arrows;

            currentRoom.Gold = currentRoom.Arrows = 0;
        }

        /// <summary>
        ///     Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(Direction dir)
        {
            return MovePlayer((int) dir);
        }

        /// <summary>
        ///     Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(SquareDirection dir)
        {
            return MovePlayer((int) dir);
        }

        /// <summary>
        ///     Moves a player to the specified room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Whether the move was valid.</returns>
        public bool MovePlayerTo(int roomId)
        {
            if (Cave.GetRoom(roomId) == null)
                return false;

            PlayerRoom = roomId;
            ProcessPlayerMove();

            return true;
        }

        /// <summary>
        ///     Takes a room ID and determines whether the player can shoot to that room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Whether the player can shoot into the given room (if the player is adjacent to the room).</returns>
        public bool CanShootTo(int roomId)
        {
            return roomId != -1 && Cave.GetRoom(PlayerRoom).AdjacentRooms.Contains(roomId);
        }

        /// <summary>
        ///     Gives all warnings that the player should know about. This includes if the player is one (accessable) tile away
        ///     from:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>a pit</description>
        ///         </item>
        ///         <item>
        ///             <description>a bat</description>
        ///         </item>
        ///         <item>
        ///             <description>The wumpus</description>
        ///         </item>
        ///     </list>
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
        ///     Returns a string description of a warning
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static string GetWarningDescription(PlayerWarnings warning)
        {
            switch (warning)
            {
                case PlayerWarnings.Pit:
                    return "I feel a draft.";
                case PlayerWarnings.Bat:
                    return "Bats nearby.";
                case PlayerWarnings.Wumpus:
                    return "I smell a Wumpus!";

                default:
                    throw new Exception();
            }
        }
    }
}