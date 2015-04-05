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
           // Using a "square" pattern for now to simplify initial test code
            map.Cave.addRoom(0, new int[] { -1, 1, -1, -1 });
            map.Cave.addRoom(1, new int[] { 2, 3, -1, 0 });
            map.Cave.addRoom(2, new int[] { -1, -1, 1, -1 });
            map.Cave.addRoom(3, new int[] { -1, 4, -1, 1 });
            map.Cave.addRoom(4, new int[] { -1, 5, -1, 3 });
            map.Cave.addRoom(5, new int[] { -1, -1, -1, 4 });
            
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
