using HuntTheWumpus.SharedCode.GameMap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}
