using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HuntTheWumpus.SharedCode.GameMap;

namespace HuntTheWumpusTests
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void MapCreationTest()
        {
            Map map = new Map();
            Assert.IsNotNull(map.Cave);
            Assert.IsNotNull(map.Player);
            Assert.IsNotNull(map.Wumpus);
        }
    }
}
