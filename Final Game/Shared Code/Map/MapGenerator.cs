using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GameMap
{
    class MapGenerator
    {
        public void generateMap(Map map)
        {
            //for now just makes a preset map
            map.Cave = new Cave();

            map.Cave.AddRoom(0, new int[] { -1, 1, -1, -1, -1, -1 });
            map.Cave.AddRoom(1, new int[] { -1, 2, -1, -1, 0, -1 }, bats: true);
            map.Cave.AddRoom(2, new int[] { -1, -1, 3, -1, 1, -1 }, pit: true);
            map.Cave.AddRoom(3, new int[] { -1, -1, 4, -1, -1, 2 }, gold: 5);
            map.Cave.AddRoom(4, new int[] { -1, -1, -1, -1, -1, 3 }, arrows: 5);
            
        }
        /// <summary>
        /// For working on random caves when needed (not used at this time)
        /// </summary>
        /// <returns>randomly generated cave</returns>
        public List<Room> generateRandomCave()
        {
            List<Room> newCave = new List<Room>(29);
            List<int> allAdjacentRooms = new List<int>();
            foreach (Room x in newCave)
            {
                foreach (int y in x.adjacentRooms)
                {
                    allAdjacentRooms.Add(y);
                }
            }
            foreach (Room x in newCave)
            {
                allAdjacentRooms.Sort();
            }
            return newCave;
        }
    }
}
