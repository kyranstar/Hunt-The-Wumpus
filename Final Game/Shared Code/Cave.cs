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
                return CheckIfValid() == CaveLayoutStatus.None;
            }
        }

        public CaveLayoutStatus CheckIfValid(int NumMinConnections = -1, int NumMaxConnections = -1)
        {
            IDictionary<int, Room> cave = this.RoomDict;

            // Make sure that all connections lead to valid rooms,
            //  and that all connections are bi-directional
            bool AllConnectionsValid = cave.Values.All<Room>(
                // For each room connected to the current one
                   (e) => e.AdjacentRooms.All(
                       (r) => r == -1 || (cave.ContainsKey(r) && cave[r].AdjacentRooms.Contains(e.RoomID))
                   ));

            Dictionary<int, int> ValidatedRooms = cave.ToDictionary(pair => pair.Key, pair => 0);
            List<int> UnmarkedRooms = ValidatedRooms.Keys.ToList();

            Action<Room> MarkConnectedRooms = null;
            MarkConnectedRooms = (Room Room) =>
            {
                if (!UnmarkedRooms.Contains(Room.RoomID))
                    return;
                UnmarkedRooms.Remove(Room.RoomID);

                foreach (int ID in (from TmpID in Room.AdjacentRooms where TmpID >= 0 select TmpID))
                {
                    ValidatedRooms[ID]++;
                    MarkConnectedRooms(this[ID]);
                }

            };

            MarkConnectedRooms(this.Rooms[0]);

            int[] UnreachableRooms = UnmarkedRooms.ToArray();

            int[] TooFewConnections = ValidatedRooms
                .Where(Pair => NumMinConnections != -1 && Pair.Value < NumMinConnections)
                .Select(Pair => Pair.Key)
                .ToArray();

            int[] TooManyConnections = ValidatedRooms
                .Where(Pair => NumMaxConnections != -1 && Pair.Value > NumMaxConnections)
                .Select(Pair => Pair.Key)
                .ToArray();
            
            CaveLayoutStatus Result = CaveLayoutStatus.None;

            if (UnreachableRooms.Length > 0)
                Result |= CaveLayoutStatus.UnreachableRooms;

            if (TooFewConnections.Length > 0)
                Result |= CaveLayoutStatus.TooFewConnections;

            if (TooManyConnections.Length > 0)
                Result |= CaveLayoutStatus.TooManyConnections;

            if (!AllConnectionsValid)
                Result |= CaveLayoutStatus.MismatchedConnections;

            return Result;
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
                if (value.Length == 0)
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
            return otherRoom != null && roomID == otherRoom.roomID;
        }
        public override int GetHashCode()
        {
            return roomID;
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
