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
        private void InitializeTest()
        {
            // Using a "square" pattern for now to simplify initial test code
            TestCave.addRoom(0, new int[] { -1, 1, -1, -1 });
            TestCave.addRoom(1, new int[] { 2, 3, -1, 0 });
            TestCave.addRoom(2, new int[] { -1, -1, 1, -1 });
            TestCave.addRoom(3, new int[] { -1, 4, -1, 1 });
            TestCave.addRoom(4, new int[] { -1, 5, -1, 3 });
            TestCave.addRoom(5, new int[] { -1, -1, -1, 4 });
        }


        readonly Dictionary<int, Vector2> ExpectedRoomPoints = new Dictionary<int, Vector2>
        {
            {2, new Vector2(2, 2)},
            {5, new Vector2(8, 0)},
            {4, new Vector2(6, 0)},
            {3, new Vector2(4, 0)},
            {0, new Vector2(0, 0)},
            {1, new Vector2(2, 0)},
        };
        #endregion

        [TestMethod]
        public void MapCreationTest()
        {
            Map map = new Map();
            Assert.IsNotNull(map.Cave);
            Assert.IsNotNull(map.Player);
            Assert.IsNotNull(map.Wumpus);
        }

        [TestMethod]
        public void TestMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 1, 4, 2);

            Map.Cave = TestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(ExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(ExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys));
            // Validate each individual vector
            foreach (var Val in ExpectedRoomPoints)
                AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key]);
        }

        private void AssertVector(Vector2 Expected, Vector2 Actual, float Threshold = 0.01f)
        {
            Assert.AreEqual(Expected.X, Actual.X, Threshold);
            Assert.AreEqual(Expected.Y, Actual.Y, Threshold);
        }
    }
}
