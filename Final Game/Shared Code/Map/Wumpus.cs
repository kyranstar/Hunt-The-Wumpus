
using HuntTheWumpus.SharedCode.GameControl;
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

        /// <summary>
        /// The current location of the Wumpus (Room ID)
        /// </summary>
        public int Location
        {
            set;
            get;
        }
        private Cave cave;

        /// <summary>
        /// Initializes a new instance of the Wumpus class.
        /// </summary>
        /// <param name="cave"></param>
        public Wumpus(Map map)
        {
            this.cave = map.Cave;
            ACTIVE_BEHAVIOR = new ActiveWumpusBehavior(this, map);
            PASSIVE_BEHAVIOR = new PassiveWumpusBehavior(this, map);

            currentBehavior = PASSIVE_BEHAVIOR;
        }
        /// <summary>
        /// Moves the Wumpus to a nearby valid position
        /// </summary>
        public void Move()
        {
            currentBehavior.Move();
            if (currentBehavior == ACTIVE_BEHAVIOR && (currentBehavior as ActiveWumpusBehavior).TurnsActive >= MAX_TURNS_ACTIVE)
            {
                currentBehavior = PASSIVE_BEHAVIOR;
            }
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
            ACTIVE_BEHAVIOR.TurnsActive = 0;

            Debug.Assert(oldLocation != Location);
        }
        private interface WumpusBehavior
        {
            void Move();
        }
        private class ActiveWumpusBehavior : WumpusBehavior
        {
            public int TurnsActive = 0;

            private Wumpus wumpus;
            private Map map;
            public ActiveWumpusBehavior(Wumpus wumpus, Map cave)
            {
                this.wumpus = wumpus;
                this.map = cave;
            }
            void WumpusBehavior.Move()
            {
                TurnsActive++;

                Random r = new Random();
                var validNeighbors = map.Cave[wumpus.Location].AdjacentRooms.
                    OrderBy((a) => r.Next()).
                    Where((a) => a != -1 && !(map.Cave[a].HasPit || map.Cave[a].HasBats || map.PlayerRoom == a));
                if (validNeighbors.ToList().Count == 0)
                {
                    Log.Warn("Wumpus is not able to move!");
                    return;
                }
                else
                {
                    wumpus.Location = validNeighbors.First();
                }
            }
        }
        private class PassiveWumpusBehavior : WumpusBehavior
        {
            private Wumpus wumpus;
            private Map map;
            public PassiveWumpusBehavior(Wumpus wumpus, Map cave)
            {
                this.wumpus = wumpus;
                this.map = cave;
            }
            void WumpusBehavior.Move()
            {

            }
        }
    }
}
