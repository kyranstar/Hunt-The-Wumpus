
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// This class represents the Wumpus in a game.
    /// </summary>
    public class Wumpus
    {
        /// <summary>
        /// The number of turns the wumpus is active before reverting to passive.
        /// </summary>
        private const int MAX_TURNS_ACTIVE = 5;

        private readonly ActiveWumpusBehavior ACTIVE_BEHAVIOR;
        private readonly PassiveWumpusBehavior PASSIVE_BEHAVIOR;

        private WumpusBehavior currentBehavior;

        private readonly Cave cave;
        private readonly Map map;

        /// <summary>
        /// Initializes a new instance of the Wumpus class.
        /// </summary>
        /// <param name="map"></param>
        public Wumpus(Map map)
        {
            cave = map.Cave;
            this.map = map;

            ACTIVE_BEHAVIOR = new ActiveWumpusBehavior(this, map);
            PASSIVE_BEHAVIOR = new PassiveWumpusBehavior(this, map);

            currentBehavior = ACTIVE_BEHAVIOR;
        }

        /// <summary>
        /// The current location of the Wumpus (Room ID)
        /// </summary>
        public int Location
        {
            set;
            get;
        }

        public bool Active
        {
            get
            {
                return currentBehavior == ACTIVE_BEHAVIOR;
            }
            set
            {
                if (value)
                {
                    currentBehavior = ACTIVE_BEHAVIOR;
                }
                else
                {
                    currentBehavior = PASSIVE_BEHAVIOR;
                }
            }

        }

        /// <summary>
        /// Moves the Wumpus to a nearby valid position
        /// </summary>
        public void Move()
        {
            currentBehavior.Move();
        }

        /// <summary>
        /// Call this when the wumpus is in the same room as the player in order to move it.
        /// </summary>
        public void HitPlayer()
        {
            MoveAway(2, 4);
        }

        /// <summary>
        /// Moves the wumpus lowerBound-upperBound tiles away from its current location, both inclusive.
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        public void MoveAway(int lowerBound, int upperBound)
        {
            int oldLocation = Location;

            Random r = new Random();
            //Order rooms randomly
            foreach (int i in Enumerable.Range(0, cave.RoomDict.Count).OrderBy(x => r.Next()))
            {
                KeyValuePair<int, Room> pair = cave.RoomDict.ElementAt(i);

                //Don't move the wumpus to a room with hazards
                if (pair.Value.HasBats || pair.Value.HasPit) continue;

                int? distance = cave.Distance(pair.Value, cave[oldLocation], true);
                if (distance.HasValue && distance.Value >= lowerBound && distance.Value <= upperBound)
                {
                    Location = pair.Key;
                    break;
                }
            }
            currentBehavior = ACTIVE_BEHAVIOR;

            Debug.Assert(oldLocation != Location);
        }

        /// <summary>
        /// Moves the wumpus to a random room.
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        public void MoveToRandomRoom()
        {
            int oldLocation = Location;

            Random r = new Random();
            //Order rooms randomly
            foreach (int i in Enumerable.Range(0, cave.RoomDict.Count).OrderBy(x => r.Next()))
            {
                KeyValuePair<int, Room> pair = cave.RoomDict.ElementAt(i);

                //Don't move the wumpus to a room with hazards
                if (pair.Value.HasBats || pair.Value.HasPit || pair.Key == map.PlayerRoom) continue;

                Location = pair.Key;
            }
            if (oldLocation == Location)
            {
                Log.Error("Wumpus was not able to move in MoveToRandomRoom()");
            }
        }

        private interface WumpusBehavior
        {
            /// <summary>
            /// Moves the wumpus to a new location. 
            /// </summary>
            void Move();
            /// <summary>
            /// An arrow was show at the wumpus, but missed
            /// </summary>
            void ArrowMissed();

            /// <summary>
            /// Called when the wumpus was defeated in trivia (collision with player trivia)
            /// </summary>
            void DefeatedInTrivia();
        }

        private class ActiveWumpusBehavior : WumpusBehavior
        {
            private readonly Random Rand = new Random();
            private readonly Map Map;
            private readonly Wumpus Wumpus;
            /// <summary>
            /// Holds the number of turns the wumpus has until he starts moving again.
            /// </summary>
            private int TurnsUntilMove;
            /// <summary>
            /// Holds the number of turns the wumpus will be moving for.
            /// </summary>
            private int TurnsMoving;

            /// <summary>
            /// Holds the number of turns the wumpus has left until it stops running from being defeated in triia.
            /// </summary>
            private int TurnsMovingFromTrivia;

            public ActiveWumpusBehavior(Wumpus wumpus, Map cave)
            {
                Wumpus = wumpus;
                Map = cave;
            }

            void WumpusBehavior.Move()
            {
                if (TurnsMovingFromTrivia <= 0)
                {
                    BasicMove();
                }
                else
                {
                    TriviaMove();
                }
            }
            private void TriviaMove()
            {
                TurnsMovingFromTrivia--;
                //If the Wumpus is defeated in trivia, it will run up to two rooms away per turn for up to three turns.
                var validRooms = Map.Cave.Rooms
                        .Select(r => r.RoomID)
                    // Valid rooms
                        .Where(a =>
                            a != -1
                            && !Map.Cave[a].HasPit
                            && !Map.Cave[a].HasBats
                            && Map.PlayerRoom != a
                            && Wumpus.Location != a)
                    // Rooms up to two tiles away
                       .Where(a =>
                            Pathfinding.FindPath(Map.Cave[a], Map.Cave[Wumpus.Location], Map.Cave, false).Count <= 2)
                        .ToList();
                if (validRooms.Count == 0)
                {
                    Log.Warn("Active wumpus is not able to run from a defeat in trivia!");
                }
                else
                {
                    Wumpus.Location = validRooms.GetRandom();
                    Log.Info("Active wumpus ran from defeat in trivia to room " + Wumpus.Location);
                }
            }

            private void BasicMove()
            {
                // Every turn, there is a 5% chance the Wumpus will immediately teleport to a new, random location.
                if (Rand.Next(100) < 5)
                {
                    var validRooms = Map.Cave.Rooms
                        .Select(r => r.RoomID)
                        .Where(a =>
                            a != -1
                            && !Map.Cave[a].HasPit
                            && !Map.Cave[a].HasBats
                            && Map.PlayerRoom != a
                            && Wumpus.Location != a)
                        .ToList();

                    if (validRooms.Count == 0)
                    {
                        Log.Warn("Active wumpus is not able to move randomly!");
                    }
                    else
                    {
                        Wumpus.Location = validRooms.GetRandom();
                        Log.Info("Active wumpus moved randomly to room " + Wumpus.Location);
                    }
                }
                // Every 5 to 10 turns the Wumpus will wake up and move 1 room per turn for up to three turns before going back to sleep.
                if (TurnsMoving > 0 || TurnsUntilMove <= 0)
                {
                    var validNeighbors = Map.Cave[Wumpus.Location].AdjacentRooms
                        .Where(a =>
                            a != -1
                            && Map.Cave.RoomDict.ContainsKey(a)
                            && !Map.Cave[a].HasPit
                            && !Map.Cave[a].HasBats
                            && Map.PlayerRoom != a)
                        .ToList();

                    if (validNeighbors.Count == 0)
                    {
                        Log.Warn("Active wumpus is not able to move!");
                    }
                    else
                    {
                        Wumpus.Location = validNeighbors.GetRandom();
                        Log.Info("Active wumpus moved to room " + Wumpus.Location);
                    }
                }

                //If we started moving this turn
                if (TurnsUntilMove <= 0)
                {
                    TurnsMoving = Rand.Next(3) + 1; // 1,2,3
                    TurnsUntilMove = Rand.Next(6) + 5; // 5 - 10 inclusive
                }
                // We started moving earlier
                else if (TurnsMoving > 0)
                {
                    TurnsMoving--;
                }
                else
                {
                    TurnsUntilMove--;
                }
            }
            void WumpusBehavior.ArrowMissed()
            {

            }

            void WumpusBehavior.DefeatedInTrivia()
            {
                //If the Wumpus is defeated in trivia, it will run up to two rooms away per turn for up to three turns.
                TurnsMovingFromTrivia = Rand.Next(3) + 1; // 1,2,3
            }
        }

        private class PassiveWumpusBehavior : WumpusBehavior
        {
            private readonly Map map;
            private readonly Wumpus Wumpus;

            public PassiveWumpusBehavior(Wumpus wumpus, Map cave)
            {
                Wumpus = wumpus;
                map = cave;
            }

            // The Wumpus is slow and can only move one room per turn??

            void WumpusBehavior.Move()
            {
                //If the Wumpus does not move for two turns, it falls asleep.
            }
            void WumpusBehavior.ArrowMissed()
            {
                // If the player shoots an arrow and misses while the Wumpus is sleeping, the Wumpus wakes up and runs up to two rooms away from current position.
            }

            void WumpusBehavior.DefeatedInTrivia()
            {
                // If the Wumpus is defeated in trivia, it will run up to three rooms away.
            }
        }
    }
}
