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

        /// <summary>
        /// Gets the calculated positions for the available room IDs
        /// </summary>
        public Dictionary<int, RoomLayoutMapping> RoomLayout
        {
            get;
            protected set;
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
        /// returns current cave on request
        /// </summary>
        /// <returns>current cave</returns>
        public List<Room> GetRoomList()
        {
            return cave.Values.ToList<Room>();
        }
        /// <summary>
        /// Returns the current cave in dictionary form (roomId -> room)
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, Room> GetRoomDict()
        {
            return cave;
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
        /// Method to randomly generate cave (work in progress, feel free to pitch in)
        /// </summary>
        /// <param name="rooms"># of rooms needed in cave</param>
        /// <returns>randomly generated cave</returns>
        // Requirements for cave:
        // In list of all room connections, each room must appear 1/2 # of rooms times
        // Each room must have at least 1, no more than 3 doors
        public Cave RandomCaveGen(int rooms)
        {
            Cave randomCave = new Cave();
            return randomCave;
        }

        /// <summary>
        /// Updates the internal layout calculations (adapts to new cave connections)
        /// </summary>
        public void RegenerateLayout()
        {
            RoomLayout = MapUtils.GetRoomLayout(cave.Values.ToArray(), RoomBaseApothem, RoomNumSides, TargetRoomWidth, TargetRoomHeight);
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
        /// how many arrows the room contains (arrows >= 0)
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
        /// true if room contains bats, false if not
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
        /// true if room contains a pit, false if not
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
        /// what other rooms this room is connected to
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
        // We should probably make these methods more robust
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
}
