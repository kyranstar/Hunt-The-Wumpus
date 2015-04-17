using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GUI;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using HuntTheWumpus.SharedCode;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpusTests
{
    [TestClass]
    public class MapTest
    {
        #region Constant test data
        const float FloatThreshold = 0.0001f;

        readonly Cave SquareTestCave = new Cave();
        readonly Cave HexTestCave = new Cave();

        [TestInitialize()]
        public void InitializeTest()
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
        readonly Dictionary<int, Vector2> SquareExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            {0, new Vector2(0, 0)},
            {1, new Vector2(2, 0)},
            {2, new Vector2(2, -2)},
            {3, new Vector2(4, 0)},
            {4, new Vector2(6, 0)},
            {5, new Vector2(8, 0)},
        };
        readonly Dictionary<int, Vector2> HexExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            // Because we are using hexagons, Y values will be even numbers but X values won't
            //  so we're using values that appear to be correct, but haven't been confirmed.
            {0, new Vector2(0, 0)},
            {1, new Vector2(1.732051f, -1)},
            {2, new Vector2(3.464102f, -2)},
            {3, new Vector2(5.196152f, -1)},
        };
        #endregion

        [TestMethod]
        public void TestMapCreation()
        {
            Map map = new Map();
            Assert.IsNotNull(map.Cave);
            Assert.IsNotNull(map.Player);
            Assert.IsNotNull(map.Wumpus);
        }
        [TestMethod]
        public void TestSquareMovePlayer()
        {
            Map map = new Map();
            map.Cave = SquareTestCave;

            // Make sure that we are in room #0
            Assert.AreEqual(0, map.PlayerRoom);
            // There should be only one direction that we can go
            AssertCannotMove(Map.SquareDirection.North, map);
            AssertCannotMove(Map.SquareDirection.West, map);
            AssertCannotMove(Map.SquareDirection.South, map);

            // Move to the next room and make sure that we moved
            AssertCanMove(Map.SquareDirection.East, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // Move up and make sure that we were successful
            AssertCanMove(Map.SquareDirection.North, map);
            Assert.AreEqual(2, map.PlayerRoom);

            // Move back down
            AssertCanMove(Map.SquareDirection.South, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // We shouldn't be able to move down again
            AssertCannotMove(Map.SquareDirection.South, map);
        }
        [TestMethod]
        public void TestHexMovePlayer()
        {
            Map map = new Map();
            map.Cave = HexTestCave;

            // Make sure that we are in room #0
            Assert.AreEqual(0, map.PlayerRoom);
            // There should be only one direction that we can go
            AssertCannotMove(Map.Direction.North, map);
            AssertCannotMove(Map.Direction.Southeast, map);
            AssertCannotMove(Map.Direction.South, map);
            AssertCannotMove(Map.Direction.Southwest, map);
            AssertCannotMove(Map.Direction.Northwest, map);

            // Move to the next room and make sure that we moved
            AssertCanMove(Map.Direction.Northeast, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // Move up and make sure that we were successful
            AssertCanMove(Map.Direction.Northeast, map);
            Assert.AreEqual(2, map.PlayerRoom);

            // Move to the next room and make sure that we moved
            AssertCanMove(Map.Direction.Southeast, map);
            Assert.AreEqual(3, map.PlayerRoom);
        }

        [TestMethod]
        public void TestSquareMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 4, 1);

            Map.Cave = SquareTestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(SquareExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(SquareExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in SquareExpectedRoomPoints)
                AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key].RoomPosition, 4);
        }

        [TestMethod]
        public void TestHexMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 6, 1);

            Map.Cave = HexTestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(HexExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(HexExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in HexExpectedRoomPoints)
                AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key].RoomPosition);
        }

        [TestMethod]
        public void TestSideMathSquare()
        {
            int NumSides = 4;

            // Angles of midpoints in degrees.
            double[] midpointAngles = {90,
                                  0,
                                  270,
                                  180};
            // Convert angles to radians
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                midpointAngles[i] = MathUtils.Mod(MathUtils.ToRadians(midpointAngles[i]), Math.PI * 2);
            }
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                //Test side angles
                Assert.AreEqual(midpointAngles[i], MapRenderer.GetAngleForSide(i, NumSides), FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapRenderer.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI / NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapRenderer.GetAngleForSectionRadius(i, NumSides), FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapRenderer.GetOffsetForSectionRadius(i, 1, NumSides));
            }

        }
        [TestMethod]
        public void TestSideMathHex()
        {
            int NumSides = 6;

            // Angles of midpoints in degrees.
            double[] midpointAngles = {30 - 60 * -1,
                                  30 - 60 * 0,
                                  30 - 60 * 1,
                                  30 - 60 * 2,
                                  30 - 60 * 3,
                                  30 - 60 * 4};
            // Convert angles to radians
            for(int i = 0; i < midpointAngles.Length; i++)
            {
                midpointAngles[i] = MathUtils.Mod(MathUtils.ToRadians(midpointAngles[i]), Math.PI * 2);
            }
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                //Test side angles
                Assert.AreEqual(midpointAngles[i], MapRenderer.GetAngleForSide(i, NumSides), FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapRenderer.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI/NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapRenderer.GetAngleForSectionRadius(i, NumSides), FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapRenderer.GetOffsetForSectionRadius(i, 1, NumSides));
            }

        }

        #region Utils
        private static void AssertMovement(bool shouldMove, int dir, Map map)
        {
            int originalPos = map.PlayerRoom;

            Assert.IsTrue(map.MovePlayer(dir) == shouldMove);
            Assert.IsTrue(originalPos == map.PlayerRoom != shouldMove);
        }

        private static void AssertCannotMove(Map.Direction dir, Map map)
        {
            AssertMovement(false,(int) dir, map);
        }
        private static void AssertCannotMove(Map.SquareDirection dir, Map map)
        {
            AssertMovement(false, (int)dir, map);
        }
        private static void AssertCanMove(Map.Direction dir, Map map)
        {
            AssertMovement(true, (int)dir, map);
        }
        private static void AssertCanMove(Map.SquareDirection dir, Map map)
        {
            AssertMovement(true, (int)dir, map);
        }


        private static void AssertVector(Vector2 Expected, Vector2 Actual, float Threshold = FloatThreshold)
        {
            Assert.AreEqual(Expected.X, Actual.X, Threshold);
            Assert.AreEqual(Expected.Y, Actual.Y, Threshold);
        }
        #endregion
    }
}
