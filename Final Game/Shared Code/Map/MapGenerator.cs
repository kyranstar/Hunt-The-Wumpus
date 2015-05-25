using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GameMap
{
    static class MapGenerator
    {
        private static readonly Random Rand = new Random();
        const int GridWidth = 6;
        const int GridHeight = 5;

        const int MinRoomConnections = 1, MaxRoomConnections = 3;

        /// <summary>
        /// Generates a random cave layout.
        /// </summary>
        /// <returns>A randomly generated cave that conforms to the spec.</returns>
        public static Cave GenerateRandomCave()
        {
            // Generation parameters
            const int roomCount = 30;
            const int maxConnectionsToCreatePerRoom = 3;

            // Generate the cave
            Cave NewCave = CreateRooms(roomCount, maxConnectionsToCreatePerRoom);
            AddHazards(NewCave);
            AddGold(NewCave);

            if (!NewCave.IsValid)
            {
                Log.Error("Nonvalid cave was generated. Errors: " + GetErrors(NewCave));
            }
            Log.Info("Cave has " + NewCave.Rooms.Count(r => r.HasPit) + " pits and " + NewCave.Rooms.Count(r => r.HasBats) + " bats.");
            return NewCave;
        }

        /// <summary>
        /// Creates a cave without hazards and gold.
        /// </summary>
        /// <param name="roomsToCreate"></param>
        /// <param name="maxConnectionsToCreatePerRoom"></param>
        /// <returns></returns>
        private static Cave CreateRooms(int roomsToCreate, int maxConnectionsToCreatePerRoom)
        {
            Cave NewCave = new Cave();
            int numRoomsCreated = 0;
            int id = 0;
            int lastRoom = 0;

            // Must be at least maxRoomCount * 2. 3 to be safe
            int size = roomsToCreate * 3;

            // Holds all the rooms that exist, -1 if no room
            int[,] rooms = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    rooms[i, j] = -1;
                }
            }

            //Start in the middle
            Point lastPos = new Point(size / 2, size / 2);
            NewCave.AddRoom(id, Enumerable.Repeat(-1, 6).ToArray());
            rooms[lastPos.X, lastPos.Y] = id;
            id++;
            // Iterate while we still need to make more rooms
            while (true)
            {
                int numToCreate = Rand.Next(maxConnectionsToCreatePerRoom);
                for (int j = 0; j < numToCreate; j++)
                {
                    int direction = Rand.Next(6);
                    int inverseDirection = GetInverseDirection(direction);

                    Point newPoint = GetDifference(lastPos.X, lastPos.Y, direction);

                    if (rooms[newPoint.X, newPoint.Y] >= 0)
                    {
                        // If we can add more connections
                        if (NewCave[lastRoom].AdjacentRooms.Count(i => i != -1) < maxConnectionsToCreatePerRoom &&
                            NewCave[rooms[newPoint.X, newPoint.Y]].AdjacentRooms.Count(i => i != -1) < maxConnectionsToCreatePerRoom)
                        {
                            //We ran into an old room. Create a connection between us and it.
                            NewCave[lastRoom].AdjacentRooms[direction] = rooms[newPoint.X, newPoint.Y];
                            NewCave[rooms[newPoint.X, newPoint.Y]].AdjacentRooms[GetInverseDirection(direction)] = lastRoom;
                        }

                        if (j == numToCreate - 1)
                        {
                            //Set last room to current room
                            lastRoom = rooms[newPoint.X, newPoint.Y];
                            lastPos = newPoint;
                        }
                    }
                    else
                    {
                        //Set connections
                        int[] connections = Enumerable.Repeat(-1, 6).ToArray();
                        connections[inverseDirection] = lastRoom;
                        NewCave[lastRoom].AdjacentRooms[direction] = id;

                        //Add room
                        NewCave.AddRoom(id, connections);
                        numRoomsCreated++;
                        // If we've created enough rooms
                        if (numRoomsCreated >= roomsToCreate)
                        {
                            // Create side connections and we're done
                            // Algorithm is giving weird results right now
                            // AddSideConnections(NewCave, maxConnectionsToCreatePerRoom);
                            return NewCave;
                        }

                        rooms[newPoint.X, newPoint.Y] = id;
                        if (j == numToCreate - 1)
                        {
                            //Set last room to current room
                            lastRoom = id;
                            lastPos = newPoint;
                        }

                        id++;
                    }
                }
            }
        }
        /// <summary>
        /// Adds connections from some rooms on the sides of the cave to the other side.
        /// </summary>
        /// <param name="cave">The cave</param>
        /// <param name="maxRoomConnections">The max amount of connections per room</param>
        private static void AddSideConnections(Cave cave, int maxRoomConnections)
        {
            var layouts = cave.RoomLayout.Values;
            // x => (lowestY, highestY, idLowest, idHighest)
            var xToLowestAndHighestY = new Dictionary<int, Tuple<int, int, int, int>>();

            // Set the dictionary to correct values
            foreach (var layout in layouts)
            {
                // Makes floats less likely to conflict when cast? Probably?
                const int conflictNumber = 10000;
                int x = (int)(layout.RoomPosition.X * conflictNumber);
                int y = (int)(layout.RoomPosition.Y * conflictNumber);

                int id = layout.Room.RoomID;

                if (!xToLowestAndHighestY.ContainsKey(x))
                {
                    xToLowestAndHighestY[x] = new Tuple<int, int, int, int>(y, y, id, id);
                    continue;
                }
                if (y < xToLowestAndHighestY[x].Item1)
                {
                    xToLowestAndHighestY[x] = new Tuple<int, int, int, int>(y, xToLowestAndHighestY[x].Item2, id, xToLowestAndHighestY[x].Item4);
                }
                if (y > xToLowestAndHighestY[x].Item2)
                {
                    xToLowestAndHighestY[x] = new Tuple<int, int, int, int>(xToLowestAndHighestY[x].Item1, y, xToLowestAndHighestY[x].Item3, id);
                }
            }
            foreach (var range in xToLowestAndHighestY)
            {
                int idMin = range.Value.Item3;
                int idMax = range.Value.Item4;
                if (cave[idMin].AdjacentRooms.Count(r => r != -1) >= maxRoomConnections ||
                    cave[idMax].AdjacentRooms.Count(r => r != -1) >= maxRoomConnections)
                {
                    continue;
                }
                if (cave[idMin].AdjacentRooms[(int)Direction.North] == -1 && cave[idMax].AdjacentRooms[(int)Direction.South] == -1)
                {
                    if (Rand.Next(100) < 50) continue;

                    // Make connection
                    cave[idMin].AdjacentRooms[(int)Direction.North] = idMax;
                    cave[idMax].AdjacentRooms[(int)Direction.South] = idMin;
                }
            }

        }
        /// <summary>
        /// Populates a cave with gold
        /// </summary>
        /// <param name="NewCave"></param>
        private static void AddGold(Cave NewCave)
        {
            foreach (var room in NewCave.Rooms)
            {
                room.Gold = 1;
            }
        }
        /// <summary>
        /// Adds 2 pits and 2 bats according to spec.
        /// </summary>
        /// <param name="NewCave"></param>
        private static void AddHazards(Cave NewCave)
        {
            for (int i = 0; i < 2; i++)
            {
                var room = FindValidHazardSpot(NewCave);
                if (room.HasValue)
                    NewCave[room.Value].HasPit = true;
            }
            for (int i = 0; i < 2; i++)
            {
                var room = FindValidHazardSpot(NewCave);
                if (room.HasValue)
                    NewCave[room.Value].HasBats = true;
            }
        }
        /// <summary>
        /// Finds a valid spot for a hazard where it will not impede in gameplay.
        /// </summary>
        /// <param name="cave"></param>
        /// <returns>A room ID</returns>
        private static int? FindValidHazardSpot(Cave cave)
        {
            // Random order so we don't get a bunch in the beginning of the map
            foreach (Room room in cave.RoomDict.Values.OrderBy(r => Rand.Next()))
            {
                // Invalid if it already has a hazard, or if it is the player's starting position
                if (room.HasBats || room.HasPit || room.RoomID == 0) continue;

                // A room is automatically valid if it only has one connection
                if (room.AdjacentRooms.Where(r => r != -1).Count() == 1)
                {
                    return room.RoomID;
                }
            }
            return null;
        }

        private static string GetErrors(Cave cave)
        {
            var status = CaveUtils.CheckIfValid(cave.RoomDict);
            var errors = "";
            if (status.Has(CaveLayoutStatus.TooFewConnections))
            {
                errors += "Too few connections, ";
            }
            if (status.Has(CaveLayoutStatus.MismatchedConnections))
            {
                errors += "Mismatched connections, ";
            }
            if (status.Has(CaveLayoutStatus.TooManyConnections))
            {
                errors += "Too many connections, ";
            }
            if (status.Has(CaveLayoutStatus.UnreachableRooms))
            {
                errors += "Unreachable rooms, ";
            }
            return errors;
        }

        private static int GetInverseDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    return 3;
                case 1:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 0;
                case 4:
                    return 1;
                case 5:
                    return 2;
                default:
                    throw new ArgumentException("0 <= direction < 6");
            }
        }
        private static Point GetDifference(int CurrRow, int CurrCol, int Direction)
        {
            switch (Direction)
            {
                case 0:
                    return new Point(CurrRow - 1, CurrCol);
                case 1:
                    return MathUtils.IsEven(CurrCol) ? new Point(CurrRow - 1, CurrCol + 1) : new Point(CurrRow, CurrCol + 1);
                case 2:
                    return MathUtils.IsEven(CurrCol) ? new Point(CurrRow, CurrCol + 1) : new Point(CurrRow + 1, CurrCol + 1);
                case 3:
                    return new Point(CurrRow + 1, CurrCol);
                case 4:
                    return MathUtils.IsEven(CurrCol) ? new Point(CurrRow, CurrCol - 1) : new Point(CurrRow + 1, CurrCol - 1);
                case 5:
                    return MathUtils.IsEven(CurrCol) ? new Point(CurrRow - 1, CurrCol - 1) : new Point(CurrRow, CurrCol - 1);
                default:
                    throw new ArgumentException("Direction was not between 0 and 5 inclusive. Was " + Direction);
            }
        }
    }
}
