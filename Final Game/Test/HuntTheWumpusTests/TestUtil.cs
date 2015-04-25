using HuntTheWumpus.SharedCode;
using HuntTheWumpus.SharedCode.GameMap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HuntTheWumpusTests
{
    public class TestUtil
    {
        #region Constant test data
        public const float FloatThreshold = 0.0001f;

        public readonly static Cave SquareTestCave = new Cave(RoomNumSides: 4, RoomBaseApothem: 1);
        public readonly static Cave HexTestCave = new Cave(RoomNumSides: 6, RoomBaseApothem: 1);

        static TestUtil()
        {
            // Using a "square" pattern for now to simplify initial test code
            SquareTestCave.AddRoom(0, new int[] { -1, 1, -1, -1 });
            SquareTestCave.AddRoom(1, new int[] { 2, 3, -1, 0 });
            SquareTestCave.AddRoom(2, new int[] { -1, -1, 1, -1 });
            SquareTestCave.AddRoom(3, new int[] { -1, 4, -1, 1 });
            SquareTestCave.AddRoom(4, new int[] { -1, 5, -1, 3 });
            SquareTestCave.AddRoom(5, new int[] { -1, -1, -1, 4 });

            HexTestCave.AddRoom(0, new int[] { -1, 1, -1, -1, -1, -1 });
            HexTestCave.AddRoom(1, new int[] { -1, 2, -1, -1, 0, -1 });
            HexTestCave.AddRoom(2, new int[] { -1, -1, 3, -1, 1, -1 });
            HexTestCave.AddRoom(3, new int[] { -1, -1, -1, -1, -1, 2 });
        }

        /*      ___
         *     | 2 |
         *  ___|___|___ ___ ___
         * | 0 | 1 | 3 | 4 | 5 |
         * |___|___|___|___|___|
         */
        public static readonly Dictionary<int, Vector2> SquareExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            {0, new Vector2(0, 0)},
            {1, new Vector2(2, 0)},
            {2, new Vector2(2, -2)},
            {3, new Vector2(4, 0)},
            {4, new Vector2(6, 0)},
            {5, new Vector2(8, 0)},
        };
        public static readonly Dictionary<int, Vector2> HexExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            // Because we are using hexagons, Y values will be even numbers but X values won't
            //  so we're using values that appear to be correct, but haven't been confirmed.
            {0, new Vector2(0, 0)},
            {1, new Vector2(1.732051f, -1)},
            {2, new Vector2(3.464102f, -2)},
            {3, new Vector2(5.196152f, -1)},
        };
        #endregion
        #region Utils
        public static void AssertMovement(bool shouldMove, int dir, Map map)
        {
            int originalPos = map.PlayerRoom;

            Assert.IsTrue(map.MovePlayer(dir) == shouldMove);
            Assert.IsTrue(originalPos == map.PlayerRoom != shouldMove);
        }

        public static void AssertCannotMove(Map.Direction dir, Map map)
        {
            AssertMovement(false, (int)dir, map);
        }
        public static void AssertCannotMove(Map.SquareDirection dir, Map map)
        {
            AssertMovement(false, (int)dir, map);
        }
        public static void AssertCanMove(Map.Direction dir, Map map)
        {
            AssertMovement(true, (int)dir, map);
        }
        public static void AssertCanMove(Map.SquareDirection dir, Map map)
        {
            AssertMovement(true, (int)dir, map);
        }

        // Doubles just to avoid casting
        public static void AssertFloat(double a, double b, float Threshold = FloatThreshold)
        {
            Assert.AreEqual(a, b, Threshold);
        }

        public static void AssertVector(Vector2 Expected, Vector2 Actual, float Threshold = FloatThreshold)
        {
            Assert.AreEqual(Expected.X, Actual.X, Threshold);
            Assert.AreEqual(Expected.Y, Actual.Y, Threshold);
        }
        #endregion
    }
}