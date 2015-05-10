using HuntTheWumpus.SharedCode.GameMap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuntTheWumpusTests
{
    [TestClass]
    public class MapTest
    {

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
            map.Cave = TestUtil.SquareTestCave;
            map.Wumpus.SetActive(false);

            // Make sure that we are in room #0
            Assert.AreEqual(0, map.PlayerRoom);
            // There should be only one direction that we can go
            TestUtil.AssertCannotMove(Map.SquareDirection.North, map);
            TestUtil.AssertCannotMove(Map.SquareDirection.West, map);
            TestUtil.AssertCannotMove(Map.SquareDirection.South, map);

            // Move to the next room and make sure that we moved
            TestUtil.AssertCanMove(Map.SquareDirection.East, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // Move up and make sure that we were successful
            TestUtil.AssertCanMove(Map.SquareDirection.North, map);
            Assert.AreEqual(2, map.PlayerRoom);

            // Move back down
            TestUtil.AssertCanMove(Map.SquareDirection.South, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // We shouldn't be able to move down again
            TestUtil.AssertCannotMove(Map.SquareDirection.South, map);
        }
        [TestMethod]
        public void TestHexMovePlayer()
        {
            Map map = new Map();
            map.Cave = TestUtil.HexTestCave;
            map.Wumpus.SetActive(false);

            // Make sure that we are in room #0
            Assert.AreEqual(0, map.PlayerRoom);
            // There should be only one direction that we can go
            TestUtil.AssertCannotMove(Map.Direction.North, map);
            TestUtil.AssertCannotMove(Map.Direction.Southeast, map);
            TestUtil.AssertCannotMove(Map.Direction.South, map);
            TestUtil.AssertCannotMove(Map.Direction.Southwest, map);
            TestUtil.AssertCannotMove(Map.Direction.Northwest, map);

            // Move to the next room and make sure that we moved
            TestUtil.AssertCanMove(Map.Direction.Northeast, map);
            Assert.AreEqual(1, map.PlayerRoom);

            // Move up and make sure that we were successful
            TestUtil.AssertCanMove(Map.Direction.Northeast, map);
            Assert.AreEqual(2, map.PlayerRoom);

            // Move to the next room and make sure that we moved
            TestUtil.AssertCanMove(Map.Direction.Southeast, map);
            Assert.AreEqual(3, map.PlayerRoom);
        }

    }
}
