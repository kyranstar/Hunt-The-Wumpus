using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode
{
    /// <summary>
    /// The cave object generates a cave system and places objects inside it (player, arrows, gold, etc)
    /// Includes methods to get locations of game objects in cave
    /// <summary>
    class Cave
    {
        /// <summary>
        /// contains generated cave (list of rooms)
        /// </summary>
        public List<Room> cave;
        /// <summary>
        /// contains list of rooms which contain arrows
        /// </summary>
        public List<int> arrows;
        /// <summary>
        /// contains list of rooms which contain gold
        /// </summary>
        public List<int> gold;
        /// <summary>
        /// contains starting room of player
        /// </summary>
        public int playerStart;

        /// <summary>
        ///tells if room x contains gold
        /// </summary>
        /// <param name="x"></param>
        /// <returns>whether room x in cave contains gold</returns>
        public bool isThereGoldInThisRoom(int x)
        {
            foreach (int y in gold)
            {
                if (gold[x] == y)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        ///tells if room x contains arrows
        /// </summary>
        /// <param name="x">room</param>
        /// <returns>whether room x in cave contains arrows</returns>
        public bool areThereArrowsInThisRoom(int x)
        {
            foreach (int y in arrows)
            {
                if (arrows[x] == y)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// tells whether specified room x contains bats
        /// </summary>
        /// <param name="x">room ID</param>
        /// <returns>whether room contains bats</returns>
        public bool doesThisRoomContainBats(int x)
        {
            return false;
        }
        /// <summary>
        /// tells whether specified room x contains a pit
        /// </summary>
        /// <param name="x">room ID</param>
        /// <returns>whether room contains a pit</returns>
        public bool doesThisRoomContainAPit(int x)
        {
            return false;
        }

        /// <summary>
        /// generates random cave
        /// </summary>
        public void generate()
        {
        }

        /// <summary>
        /// returns current cave on request
        /// </summary>
        /// <returns>current cave</returns>
        public Room[] getCave()
        {
            return cave;
        }
    }
    /// <summary>
    /// class which represents one room which is part of the cave system
    /// </summary>
    class Room
    {
        /// <summary>
        /// room's location in cave
        /// </summary>
        private int roomId;
        /// <summary>
        /// how many doors the room has (1-3)
        /// </summary>
        private int doors;
        /// <summary>
        /// what other rooms this room is connected to
        /// </summary>
        private List<int> GetAdjacentRooms();
    }
}
