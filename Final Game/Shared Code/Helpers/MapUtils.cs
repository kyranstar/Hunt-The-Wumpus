﻿using HuntTheWumpus.SharedCode;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GameMap
{
    static class MapUtils
    {
        /// <summary>
        /// Lays out the given map by converting the room's individual connections into absolute positions
        /// </summary>
        /// <param name="Rooms">The rooms in the map</param>
        /// <returns>A mapping of room IDs to their room's positions</returns>
        public static Dictionary<int, RoomLayoutMapping> GetRoomLayout(Room[] Rooms, double RoomBaseApothem, int RoomNumSides, float TargetRoomWidth, float TargetRoomHeight)
        {
            Dictionary<int, Room> UnmappedRooms = Rooms.ToDictionary(Room => Room.roomId);
            Dictionary<int, RoomLayoutMapping> NewLayout = GetRoomLayout(Rooms[0], new Vector2(), UnmappedRooms, RoomBaseApothem, RoomNumSides, TargetRoomWidth, TargetRoomHeight);

            // If not all rooms were found, we know that not all of them have a valid connection
            if (UnmappedRooms.Count > 0)
                Log.Warn("Some rooms have no valid connections and have not been positioned! The following rooms haven't been connected: " + String.Join(", ", UnmappedRooms.Keys.ToArray()));

            return NewLayout;
        }

        /// <summary>
        /// Lays out the given map by converting the room's individual connections into absolute positions
        /// </summary>
        /// <param name="CurrentRoom">The room to start at</param>
        /// <param name="CurrentPoint">The position that the current room should be at</param>
        /// <param name="UnmappedRooms">The list of the unmapped rooms, indexed by ID</param>
        /// <returns>A mapping of room IDs to their room's positions</returns>
        public static Dictionary<int, RoomLayoutMapping> GetRoomLayout(Room CurrentRoom, Vector2 CurrentPoint, Dictionary<int, Room> UnmappedRooms, double RoomBaseApothem, int RoomNumSides, float TargetRoomWidth, float TargetRoomHeight)
        {
            Log.Info("GetRoomLayout called for room " + CurrentRoom.roomId + " at point " + CurrentPoint + " with " + UnmappedRooms.Count + " unmapped rooms");

            // Start with an empty result
            Dictionary<int, RoomLayoutMapping> NewMappedRooms = new Dictionary<int, RoomLayoutMapping>();

            // Iterate over the connections (the index in the array indicates the side)
            for (int ConnectionDirection = 0; ConnectionDirection < CurrentRoom.adjacentRooms.Length; ConnectionDirection++)
            {
                // Get the ID of the current room
                int ConnectedRoomID = CurrentRoom.adjacentRooms[ConnectionDirection];

                Room NextRoom;
                // Only process the room if it hasn't been processed already
                if (UnmappedRooms.TryGetValue(ConnectedRoomID, out NextRoom))
                {
                    // If we have gotten to this next room by reference but the next room
                    //   does not have a connection back to the first one, warn of issues!
                    if (!NextRoom.adjacentRooms.Contains(CurrentRoom.roomId))
                        Log.Warn("Room " + CurrentRoom.roomId + " claims it is connected to room " + NextRoom.roomId + ", but the inverse connection was not found!");

                    RoomLayoutMapping NextMapping = new RoomLayoutMapping()
                    {
                        Room = NextRoom
                    };

                    // Get the point for the next room
                    NextMapping.RoomPosition = CurrentPoint + GetOffsetForSide(ConnectionDirection, RoomBaseApothem * 2, RoomNumSides);

                    // Get the list of poses for the non-connection overlays (closed doors)
                    NextMapping.ClosedDoorMappings = MapDoorsForRoom(NextRoom.adjacentRooms, NextMapping.RoomPosition, RoomBaseApothem, RoomNumSides, TargetRoomWidth, TargetRoomHeight);

                    // Remove the room now that we have calculated its position
                    //   we don't want the next call to index it again
                    UnmappedRooms.Remove(ConnectedRoomID);

                    // Recurse through the connections of the next room
                    Dictionary<int, RoomLayoutMapping> MappedRooms = GetRoomLayout(NextRoom, NextMapping.RoomPosition, UnmappedRooms, RoomBaseApothem, RoomNumSides, TargetRoomWidth, TargetRoomHeight);

                    // Add the current room to the deeper map
                    MappedRooms.Add(NextRoom.roomId, NextMapping);
                    // Merge the result with the results from the other connections
                    NewMappedRooms = NewMappedRooms.MergeLeft(MappedRooms);

                }
            }

            return NewMappedRooms;
        }

        /// <summary>
        /// Gets the position and rotation (pose) of each closed door for the room info.
        /// </summary>
        /// <param name="Connections">The set of connections that the given room has</param>
        /// <param name="RoomOrigin">The position of the given room</param>
        /// <returns>A mapping of positions and rotations for each closed door</returns>
        public static Tuple<Vector2, float>[] MapDoorsForRoom(int[] Connections, Vector2 RoomOrigin, double RoomBaseApothem, int RoomNumSides, float TargetRoomWidth, float TargetRoomHeight)
        {
            List<Tuple<Vector2, float>> DoorMappings = new List<Tuple<Vector2, float>>();

            foreach (int Direction in
                Connections
                .Select((RoomID, Index) => new KeyValuePair<int, int>(Index, RoomID)) // Map the connections to <Direction, RoomID>
                .Where(AdjacentRoomMapping => AdjacentRoomMapping.Value == -1) // Only select the non-connections
                .Select(Pair => Pair.Key)) // Convert it back to an array of directions
            {

                // Get the offsets for the current direction
                Vector2 Offset = GetOffsetForSectionRadius(Direction, RoomBaseApothem, RoomNumSides);
                Vector2 CenterRoom = new Vector2()
                {
                    X = RoomOrigin.X + TargetRoomWidth / 2,
                    Y = RoomOrigin.Y + TargetRoomHeight / 2
                };

                Vector2 DoorIconPosition = new Vector2()
                {
                    X = CenterRoom.X + Offset.X,
                    Y = CenterRoom.Y + Offset.Y
                };

                // Get the rotation to make the wedge fit corectly
                float DoorIconRotation = -GetAngleForSide(Direction, RoomNumSides) + ((float)Math.PI * 0.5f);

                DoorMappings.Add(new Tuple<Vector2, float>(DoorIconPosition, DoorIconRotation));
            }

            // Can't use yield return because we need an array 
            return DoorMappings.ToArray();
        }

        /// <summary>
        /// Calculates a vector that describes the given side's middle point, relative to the center.
        /// </summary>
        /// <param name="Side">The side index</param>
        /// <param name="Apothem">The measure of the apothem of the polygon.</param>
        /// <param name="NumSides">The number of sides on the polygon.</param>
        /// <returns>A vector representing the given values.</returns>
        public static Vector2 GetOffsetForSide(int Side, double Apothem, int NumSides)
        {
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            float Angle = GetAngleForSide(Side, NumSides);
            return MathUtils.PolarToCart(Angle, Apothem);
        }

        /// <summary>
        /// Calculates the angle that points to the center of the specified side.
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        public static float GetAngleForSide(int Side, int NumSides)
        {
            double d = (Math.PI / 2f) - (Math.PI * 2f / NumSides) * Side;
            return (float)MathUtils.Mod(d, (Math.PI * 2));
        }

        /// <summary>
        /// Calculates a vector that describes the given side's corner point, relative to the center.
        /// </summary>
        /// <param name="Side">The side index.</param>
        /// <param name="Apothem">The measure of the apothem of the polygon.</param>
        /// <returns></returns>
        public static Vector2 GetOffsetForSectionRadius(int Side, double Apothem, int NumSides)
        {
            // TODO: Find a better name
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            // Assuming side 0 is the NW radius line
            float Angle = GetAngleForSectionRadius(Side, NumSides);
            double Radius = MathUtils.PolygonRadius(NumSides, Apothem);
            return MathUtils.PolarToCart(Angle, Radius);
        }

        /// <summary>
        /// Calculates the angle that points to the corner of the specified side.
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        public static float GetAngleForSectionRadius(int Side, int NumTotalSides)
        {
            double SingleSectionAngle = Math.PI * 2f / NumTotalSides;
            double Angle = (Math.PI / 2f + SingleSectionAngle / 2f) - SingleSectionAngle * Side;
            return (float)(MathUtils.Mod(Angle, Math.PI * 2));
        }
    }

    /// <summary>
    /// Holds render information about a single room.
    /// </summary>
    public class RoomLayoutMapping
    {
        public Room Room;
        public Vector2 RoomPosition;
        public Tuple<Vector2, float>[] ClosedDoorMappings;
    }
}