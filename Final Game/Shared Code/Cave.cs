using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode
{
    /// <summary>
    /// The cave object generates a cave system and places objects inside it (player, arrows, gold, etc)
    /// Includes methods to get locations of game objects in cave
    /// </summary>
    public class Cave
    {
        // Length of the apothem of each room as it should be drawn (virtual coords)
        private readonly int RoomNumSides;
        private readonly double RoomBaseApothem;
        public int TargetRoomWidth { get; protected set; }
        public int TargetRoomHeight { get; protected set; }


        public Cave(int RoomNumSides = 6, double RoomBaseApothem = 300)
        {
            this.RoomBaseApothem = RoomBaseApothem;
            this.RoomNumSides = RoomNumSides;

            this.TargetRoomWidth = (int)Math.Round(MathUtils.PolygonWidth(RoomNumSides, RoomBaseApothem));
            this.TargetRoomHeight = (int)Math.Round(MathUtils.PolygonHeight(RoomNumSides, RoomBaseApothem));
        }

        /// <summary>
        /// contains generated cave (dictionary of rooms)
        /// </summary>
        private IDictionary<int, Room> cave = new Dictionary<int, Room>();


        private Dictionary<int, RoomLayoutMapping> roomLayout;
        /// <summary>
        /// Gets the calculated positions for the available room IDs
        /// </summary>
        public Dictionary<int, RoomLayoutMapping> RoomLayout
        {
            get { 
                if(roomLayout == null)
                {
                    RegenerateLayout();
                }
                return roomLayout;
            }
            protected set {roomLayout = value;}
        }

        /// <summary>
        /// Gets the array of rooms in the cave
        /// </summary>
        public Room[] Rooms
        {
            get
            {
                return cave.Values.ToArray();
            }
        }

        public Room this[int RoomID]
        {
            get
            {
                return cave[RoomID];
            }
        }

        /// <summary>
        /// Returns the current cave in dictionary form (roomId -> room)
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, Room> RoomDict
        {
            get
            {
                return cave;
            }
        }

        /// <summary>
        /// Gets the room with the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room GetRoom(int id)
        {
            if (!cave.ContainsKey(id))
                return null;
            return cave[id];
        }
        /// <summary>
        /// Adds a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connections"></param>
        /// <param name="gold"></param>
        /// <param name="arrows"></param>
        /// <param name="bats"></param>
        /// <param name="pit"></param>
        public void AddRoom(int id, int[] connections, int gold = 0, int arrows = 0, bool bats = false, bool pit = false)
        {
            if (cave.ContainsKey(id))
            {
                throw new InvalidRoomException("Cannot have two rooms with the same ID!");
            }

            this.cave[id] = new Room(id, gold, arrows, bats, pit, connections);
        }

        /// <summary>
        /// Updates the internal layout calculations (adapts to new cave connections)
        /// </summary>
        public void RegenerateLayout()
        {
            RoomLayout = MapUtils.GetRoomLayout(cave.Values.ToArray(), RoomBaseApothem, RoomNumSides, TargetRoomWidth, TargetRoomHeight);
        }

        public bool IsValid
        {
            get
            {
                return true;
                //return CaveUtils.CheckIfValid(RoomDict) == CaveLayoutStatus.None;
            }
        }

        /// <summary>
        /// Gets the distance between two rooms using a pathfinding algorithm.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="avoidObstacles"></param>
        /// <param name="algorithm"></param>
        /// <returns>null if there is no path, the length of the path in between the rooms otherwise</returns>
        public int? Distance(Room a, Room b, bool avoidObstacles ,PathfindingAlgorithm algorithm = PathfindingAlgorithm.A_STAR)
        {
            var path = Pathfinding.FindPath(a, b, this, avoidObstacles, algorithm);
            if (path == null)
            {
                return null;
            }

            return path.Count;
        }
    }
    /// <summary>
    /// Class which represents one room which is part of the cave system
    /// </summary>
    public class Room
    {
        private int roomID;
        private int gold;
        private int arrows;
        private bool hasBats;
        private bool hasPit;
        private int[] adjacentRooms;

        /// <summary>
        /// room's location in cave
        /// </summary>
        public int RoomID
        {
            get { return roomID; }
            private set
            {
                if (roomID < 0)
                {
                    throw new InvalidRoomException("Room can't have negative ID");
                }
                roomID = value;
            }
        }
        /// <summary>
        /// how much gold the room contains (gold >= 0)
        /// </summary>
        public int Gold
        {
            get { return gold; }
            set
            {
                if (value < 0) throw new InvalidRoomException("Gold must be >= 0");
                gold = value;
            }
        }
        /// <summary>
        /// How many arrows the room contains (arrows >= 0)
        /// </summary>
        public int Arrows
        {
            get { return arrows; }
            set
            {
                if (value < 0) throw new InvalidRoomException("Arrows must be >= 0");
                arrows = value;
            }
        }
        /// <summary>
        /// True if room contains bats, false if not
        /// </summary>
        public bool HasBats
        {
            get { return hasBats; }
            private set
            {
                // If we already have a pit in this room and we're setting bats to true
                if (value && HasPit)
                {
                    throw new InvalidRoomException("Can't have bats AND pit in one room");
                }
                hasBats = value;
            }
        }
        /// <summary>
        /// True if room contains a pit, false if not
        /// </summary>
        public bool HasPit
        {
            get { return hasPit; }
            private set
            {
                // If we already have bats in this room and we're setting pit to true
                if (value && HasBats)
                {
                    throw new InvalidRoomException("Can't have bats AND pit in one room");
                }
                hasPit = value;
            }
        }
        /// <summary>
        /// Which other rooms this room is connected to, stored as RoomIds or -1 for no room
        /// </summary>
        public int[] AdjacentRooms
        {
            get { return adjacentRooms; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (!value.Any())
                {
                    throw new InvalidRoomException("Each room needs to be accessible");
                }
                adjacentRooms = value;
            }
        }

        public Room(int roomId, int gold, int arrows, bool bats, bool pit, int[] adjacentRooms)
        {
            this.roomID = roomId;
            this.gold = gold;
            this.arrows = arrows;
            this.HasBats = bats;
            this.HasPit = pit;
            this.AdjacentRooms = adjacentRooms;
        }

        public override string ToString()
        {
            return string.Format(
                    "Room {{id: {0}, has bats: {1}, has pit: {2}, gold: {3}, arrows: {4}}}",
                    roomID,
                    HasBats ? "yes" : "no",
                    HasPit ? "yes" : "no",
                    gold,
                    arrows
                );
        }
        public override bool Equals(Object other)
        {
            Room otherRoom = other as Room;
            return otherRoom != null && RoomID == otherRoom.RoomID;
        }
        public override int GetHashCode()
        {
            return RoomID;
        }
    }

    [Flags]
    public enum CaveLayoutStatus
    {
        /// <summary>
        /// There were no layout validation errors.
        /// </summary>
        None = 0,
        /// <summary>
        /// One or more rooms has a connection that
        /// isn't reciprocated by the connected room.
        /// </summary>
        MismatchedConnections = 2,
        /// <summary>
        /// One or more rooms are unreachable.
        /// </summary>
        UnreachableRooms = 4,
        /// <summary>
        /// One or more rooms does not meet
        /// the minimum number of required
        /// connections.
        /// </summary>
        TooFewConnections = 8,
        /// <summary>
        /// One or more rooms does not meet
        /// the maximum number of required
        /// connections.
        /// </summary>
        TooManyConnections = 16
    }
}
