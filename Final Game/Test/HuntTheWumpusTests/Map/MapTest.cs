using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GUI;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using HuntTheWumpus.SharedCode;

namespace HuntTheWumpusTests
{
    [TestClass]
    public class MapTest
    {
        #region Constant test data
        readonly Cave TestCave = new Cave();

        [TestInitialize()]
        public void InitializeTest()
        {
            // Using a "square" pattern for now to simplify initial test code
            TestCave.AddRoom(0, new int[] { -1, 1, -1, -1 });
            TestCave.AddRoom(1, new int[] { 2, 3, -1, 0 });
            TestCave.AddRoom(2, new int[] { -1, -1, 1, -1 });
            TestCave.AddRoom(3, new int[] { -1, 4, -1, 1 });
            TestCave.AddRoom(4, new int[] { -1, 5, -1, 3 });
            TestCave.AddRoom(5, new int[] { -1, -1, -1, 4 });
        }

        /*      ___
         *     | 2 |
         *  ___|___|___ ___ ___
         * | 0 | 1 | 3 | 4 | 5 |
         * |___|___|___|___|___|
         */
        readonly Dictionary<int, Vector2> ExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            {0, new Vector2(0, 0)},
            {1, new Vector2(2, 0)},
            {2, new Vector2(2, -2)},
            {3, new Vector2(4, 0)},
            {4, new Vector2(6, 0)},
            {5, new Vector2(8, 0)},
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
        public void TestMovePlayer()
        {
            Map map = new Map();
            map.Cave = TestCave;

            // Make sure that we are in room #0
            Assert.AreEqual(0, map.PlayerRoom);
            // There should be only one direction that we can go
            Assert.IsFalse(map.MovePlayer(Map.SquareDirection.North));
            Assert.IsFalse(map.MovePlayer(Map.SquareDirection.West));
            Assert.IsFalse(map.MovePlayer(Map.SquareDirection.South));

            // Move to the next room and make sure that we moved
            Assert.IsTrue(map.MovePlayer(Map.SquareDirection.East));
            Assert.AreEqual(1, map.PlayerRoom);

            // Move up and make sure that we were successful
            Assert.IsTrue(map.MovePlayer(Map.SquareDirection.North));
            Assert.AreEqual(2, map.PlayerRoom);

            // Move back down
            Assert.IsTrue(map.MovePlayer(Map.SquareDirection.South));
            Assert.AreEqual(1, map.PlayerRoom);

            // We shouldn't be able to move down again
            Assert.IsFalse(map.MovePlayer(Map.SquareDirection.South));
        }

        [TestMethod]
        public void TestMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 4, 1);

            Map.Cave = TestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(ExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(ExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in ExpectedRoomPoints)
                AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key]);
        }

        private void AssertVector(Vector2 Expected, Vector2 Actual, float Threshold = 0.0001f)
        {
            Assert.AreEqual(Expected.X, Actual.X, Threshold);
            Assert.AreEqual(Expected.Y, Actual.Y, Threshold);
        }
    }
}
