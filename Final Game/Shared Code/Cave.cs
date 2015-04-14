using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode
{
    /// <summary>
    /// The cave object generates a cave system and places objects inside it (player, arrows, gold, etc)
    /// Includes methods to get locations of game objects in cave
    /// </summary>
    public class Cave
    {
        /// <summary>
        /// contains generated cave (dictionary of rooms)
        /// </summary>
        private Dictionary<int, Room> cave = new Dictionary<int, Room>();

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
        public List<Room> getRoomList()
        {
            return cave.Values.ToList<Room>();
        }
        /// <summary>
        /// Returns the current cave in dictionary form (roomId -> room)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Room> getRoomDict()
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
            this.cave[id] = new Room(){
                roomId = id,
                adjacentRooms = connections,
                gold = gold,
                arrows = arrows,
                bats = bats,
                pit = pit
            };

            if (bats == true && pit == true)
            {
                //Need to throw exception here, can't have bats AND pit in one room
            }

            if (connections.Length == 0)
            {
                //Need to throw exception here, each room needs to be accessible
            }

            if (id < 0)
            {
                //Need to throw exception here, room can't have negative ID
            }
        }
        /// <summary>
        /// Method to randomly generate cave (work in progress, feel free to pitch in)
        /// </summary>
        /// <param name="rooms"># of rooms needed in cave</param>
        /// <returns>randomly generated cave</returns>
        // Requirements for cave:
        // In list of all room connections, each room must appear 1/2 # of rooms times
        // Each room must have at least 1, no more than 3 doors
        public Cave randomCaveGen(int rooms)
        {
            Cave randomCave = new Cave();
            return randomCave;
        }

    }
    /// <summary>
    /// Class which represents one room which is part of the cave system
    /// </summary>
    public class Room
    {
        /// <summary>
        /// room's location in cave
        /// </summary>
        public int roomId;
        /// <summary>
        /// how much gold the room contains (gold >= 0)
        /// </summary>
        public int gold;
        /// <summary>
        /// how many arrows the room contains (arrows >= 0)
        /// </summary>
        public int arrows;
        /// <summary>
        /// true if room contains bats, false if not
        /// </summary>
        public bool bats;
        /// <summary>
        /// true if room contains a pit, false if not
        /// </summary>
        public bool pit;
        /// <summary>
        /// what other rooms this room is connected to
        /// </summary>
        public int[] adjacentRooms;

        public override string ToString()
        {
            return string.Format(
                    "Room {{id: {0}, has bats: {1}, has pit: {2}, gold: {3}, arrows: {4}}}",
                    roomId,
                    bats ? "yes" : "no",
                    pit ? "yes" : "no",
                    gold,
                    arrows
                );
        }
    }
}
