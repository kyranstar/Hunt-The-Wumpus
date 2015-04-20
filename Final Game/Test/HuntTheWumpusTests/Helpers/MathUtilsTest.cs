using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;

namespace HuntTheWumpusTests.Helpers
{
    [TestClass]
    public class MathUtilsTest
    {
        [TestMethod]
        public void PolygonRadiusTest()
        {
            TestUtil.AssertFloat(Math.Sqrt(6 - 2 * Math.Sqrt(5)), MathUtils.PolygonRadius(5, 1));

            TestUtil.AssertFloat(0, MathUtils.PolygonRadius(6, 0));
            TestUtil.AssertFloat(2D/Math.Sqrt(3), MathUtils.PolygonRadius(6,1));

            TestUtil.AssertFloat(MathUtils.Sec(Math.PI / 15), MathUtils.PolygonRadius(15, 1));
        }
        [TestMethod]
        public void PolarToCartTest()
        {
            TestUtil.AssertVector(new Vector2(0, -1), MathUtils.PolarToCart(Math.PI / 2, 1));
            TestUtil.AssertVector(new Vector2(-1, 0), MathUtils.PolarToCart(Math.PI, 1));
            TestUtil.AssertVector(new Vector2(0, 1), MathUtils.PolarToCart(3 * Math.PI / 2, 1));
            TestUtil.AssertVector(new Vector2(1, 0), MathUtils.PolarToCart(2 * Math.PI, 1));
            TestUtil.AssertVector(new Vector2(1, 0), MathUtils.PolarToCart(0, 1));
        }
        [TestMethod]
        public void IsInsideHexagonTest()
        {
            float unitHexWidth = (float)MathUtils.PolygonWidth(6, 1);
            float unitHexHeight = (float)MathUtils.PolygonHeight(6, 1);
            float unitHexRadius = (float)MathUtils.PolygonRadius(6, 1);

            Assert.AreEqual(true, MathUtils.IsInsideHexagon(new Vector2(0, 0), new Vector2(0, 0), 1, 1));

            // left and right vertices
            Assert.AreEqual(true, MathUtils.IsInsideHexagon(new Vector2(unitHexWidth / 2, 0), new Vector2(0, 0), 1, 1));
            Assert.AreEqual(true, MathUtils.IsInsideHexagon(new Vector2(-unitHexWidth / 2, 0), new Vector2(0, 0), 1, 1));

            // top and bottom
            Assert.AreEqual(true, MathUtils.IsInsideHexagon(new Vector2(0, unitHexHeight / 2), new Vector2(0, 0), 1, 1));
            Assert.AreEqual(true, MathUtils.IsInsideHexagon(new Vector2(0, -unitHexHeight / 2), new Vector2(0, 0), 1, 1));

            // rectangle corners
            Assert.AreEqual(false, MathUtils.IsInsideHexagon(new Vector2(unitHexWidth / 2, unitHexHeight / 2), new Vector2(0, 0), 1, 1));
            Assert.AreEqual(false, MathUtils.IsInsideHexagon(new Vector2(-unitHexWidth / 2, unitHexHeight / 2), new Vector2(0, 0), 1, 1));
            Assert.AreEqual(false, MathUtils.IsInsideHexagon(new Vector2(unitHexWidth / 2, -unitHexHeight / 2), new Vector2(0, 0), 1, 1));
            Assert.AreEqual(false, MathUtils.IsInsideHexagon(new Vector2(-unitHexWidth / 2, -unitHexHeight / 2), new Vector2(0, 0), 1, 1));
        }
    }
}
