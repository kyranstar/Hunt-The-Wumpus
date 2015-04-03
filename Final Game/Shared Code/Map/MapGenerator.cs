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
            map.Cave.cave = new List<Room> 
            {
                new Room()
                {
                    roomId = 0,
                    // Using a "square" pattern for now to simplify initial test code
                    adjacentRooms = new int[] {-1, 1, -1, -1}
                },
                new Room()
                {
                    roomId = 1,
                    adjacentRooms = new int[] {2, 3, -1, 0}
                },
                new Room()
                {
                    roomId = 2,
                    adjacentRooms = new int[] {-1, -1, 1, -1}
                },
                new Room()
                {
                    roomId = 3,
                    adjacentRooms = new int[] {-1, 4, -1, 1}
                },
                new Room()
                {
                    roomId = 4,
                    adjacentRooms = new int[] {-1, 5, -1, 3}
                },
                new Room()
                {
                    roomId = 5,
                    adjacentRooms = new int[] {-1, -1, -1, 4}
                }
            };
        }
    }
}
