using HuntTheWumpus.SharedCode.GameControl;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
        ///     Holds a reference to the current player
        /// </summary>
        public Player Player { private set; get; }

        /// <summary>
        ///     Holds a reference to the wumpus
        /// </summary>
        public Wumpus Wumpus { private set; get; }

        private Cave _cave;
        public int MoveCount;

        /// <summary>
        ///     Holds the location of the player within the current room. The origin is the center of this room.
        /// </summary>
        public Point PlayerRoomLocation;

        public ISet<int> PlayerPath = new HashSet<int>();

        /// <summary>
        ///     Constructs the map and generates the cave with a MapGenerator.
        /// </summary>
        public Map()
        {
            Reset();
        }

        public void Reset()
        {
            Log.Info("Generating map...");
            Cave = MapGenerator.GenerateRandomCave();
            Wumpus = new Wumpus(this);
            Player = new Player();

            PlayerRoomLocation = new Point(0, 0);
            MoveCount = 0;

            Room FirstSafeRoom = GetPlayerStartRoom();

            if (FirstSafeRoom != null)
                PlayerRoom = FirstSafeRoom.RoomID;
            else
                Log.Error("Couldn't find valid room to move player to!");

            Wumpus.MoveToRandomRoom();
            CollectItemsFromRoom();
            PlayerPath.Clear();
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
        /// <summary>
        /// Updates the game. Called every frame
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Cave.Update(gameTime);
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

                //Wumpus has to move after the player does
                Wumpus.Move();

                // Update player tracking
                ProcessPlayerMove();

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

            Player.Turns = ++MoveCount;

            Room currentRoom = Cave[PlayerRoom];
            if (currentRoom.HasBats)
            {
                ReactToBatCollision(currentRoom);
            }

            Trivia.Trivia.UnlockNewHint();
            OnPlayerMoved(this, new EventArgs());
        }
        /// <summary>
        /// Call this when a bat collision occurs. Moves the player and bat to a nearby position as according to spec.
        /// </summary>
        /// <param name="currentRoom"></param>
        private void ReactToBatCollision(Room currentRoom)
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
        /// <summary>
        /// Returns a valid starting room for the player.
        /// </summary>
        /// <returns></returns>
        public Room GetPlayerStartRoom()
        {
            Func<Room, bool> RoomValidator = r =>
                    !r.HasBats
                    && !r.HasPit
                    && Wumpus.Location != r.RoomID
                    && PlayerRoom != r.RoomID;

            // Place player in already visited location without hazards
            Room FirstSafeRoomInPlayerPath = PlayerPath.Select(i => Cave[i])
                .FirstOrDefault(RoomValidator);

            // If there are no non-hazardous locations that we've already visited
            if (FirstSafeRoomInPlayerPath == null)
            {
                // Check all possible rooms instead
                return Cave.Rooms
                    .FirstOrDefault(RoomValidator);
            }
            else
                return FirstSafeRoomInPlayerPath;
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
            return MovePlayer((int)dir);
        }

        /// <summary>
        ///     Moves the player relatively to a new room if this room connects to that room.
        /// </summary>
        /// <param name="dir"></param>
        public bool MovePlayer(SquareDirection dir)
        {
            return MovePlayer((int)dir);
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
        public PlayerWarnings[] GetPlayerWarnings()
        {
            List<PlayerWarnings> Results = new List<PlayerWarnings>();

            int[] adjacentRooms = Cave[PlayerRoom].AdjacentRooms;
            foreach (int room in adjacentRooms.Where(c => c >= 0))
            {
                Room r = Cave[room];
                if (r.HasBats)
                    Results.Add(PlayerWarnings.Bat);
                if (r.HasPit)
                    Results.Add(PlayerWarnings.Pit);
            }

            if (adjacentRooms.Contains(Wumpus.Location))
                Results.Add(PlayerWarnings.Wumpus);

            return Results.Distinct().ToArray();
        }
    }
}