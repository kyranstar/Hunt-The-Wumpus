using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GameMap
{
    static class MapGenerator
    {
        const int GridWidth = 6;
        const int GridHeight = 5;

        const int MinRoomConnections = 1, MaxRoomConnections = 3;

        public static void generateMap(Map map)
        {
            //for now just makes a preset map
            map.Cave = new Cave();

            map.Cave.AddRoom(0, new int[] { -1, 1, 5, -1, -1, -1 });
            map.Cave.AddRoom(1, new int[] { -1, 2, -1, -1, 0, -1 }, bats: true);
            map.Cave.AddRoom(2, new int[] { -1, -1, 3, -1, 1, -1 }, pit: true);
            map.Cave.AddRoom(3, new int[] { -1, -1, 4, -1, 6, 2 }, gold: 5);
            map.Cave.AddRoom(4, new int[] { -1, -1, -1, -1, 7, 3 }, arrows: 5);
            map.Cave.AddRoom(5, new int[] { -1, 6, -1, -1, -1, 0 });
            map.Cave.AddRoom(6, new int[] { -1, 3, 7, -1, 5, -1 });
            map.Cave.AddRoom(7, new int[] { -1, 4, -1, -1, 8, 6 });
            map.Cave.AddRoom(8, new int[] { -1, 7, -1, -1, -1, -1 });

            map.Wumpus.Location = 4;
        }

        /// <summary>
        /// Generates a random cave layout.
        /// </summary>
        /// <returns>A randomly generated cave that conforms to the spec.</returns>
        public static Cave GenerateRandomCave()
        {
            // TODO: Actually randomize
            Cave NewCave = new Cave();

            for (int Col = 0; Col < GridWidth; Col++)
                for (int Row = 0; Row < GridHeight; Row++)
                {
                    Log.Info("Getting room setup for room " + Col + ", " + Row);
                    NewCave.AddRoom(GetID(Row, Col), GetConnectionsForGridRoom(Row, Col).ToArray());
                }

            return NewCave;
        }

        static IEnumerable<int> GetConnectionsForGridRoom(int Row, int Col)
        {
            for (int i = 0; i < 6; i++)
                yield return GetIDAtDirection(Row, Col, i);
        }

        static int GetIDAtDirection(int CurrRow, int CurrCol, int Direction)
        {
            // TODO: Make less atrocious
            switch (Direction)
            {
                case 0:
                    return GetID(CurrRow - 1, CurrCol);
                case 1:
                    if (MathUtils.IsEven(CurrCol))
                        return GetID(CurrRow - 1, CurrCol + 1);
                    else
                        return GetID(CurrRow, CurrCol + 1);
                case 2:
                    if (MathUtils.IsEven(CurrCol))
                        return GetID(CurrRow, CurrCol + 1);
                    else
                        return GetID(CurrRow + 1, CurrCol + 1);
                case 3:
                    return GetID(CurrRow + 1, CurrCol);
                case 4:
                    if (MathUtils.IsEven(CurrCol))
                        return GetID(CurrRow, CurrCol - 1);
                    else
                        return GetID(CurrRow + 1, CurrCol - 1);
                case 5:
                    if (MathUtils.IsEven(CurrCol))
                        return GetID(CurrRow - 1, CurrCol - 1);
                    else
                        return GetID(CurrRow, CurrCol - 1);
                default:
                    // TODO: Heed the exception
                    throw new Exception("TODO: Pick better exception");
            }
        }

        static int GetID(int Row, int Col)
        {
            if (Row < 0 || Col < 0 || Row >= GridHeight || Col >= GridWidth)
                return -1;
            else
                return Row * GridWidth + Col;
        }
    }
}
