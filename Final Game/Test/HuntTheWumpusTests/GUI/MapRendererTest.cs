using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GUI;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System;
using System.Linq;



namespace HuntTheWumpusTests.GUI
{
    [TestClass]
    public class MapRendererTest
    {
        [TestMethod]
        public void TestSquareMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 4, 1);

            Map.Cave = TestUtil.SquareTestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(TestUtil.SquareExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(TestUtil.SquareExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in TestUtil.SquareExpectedRoomPoints)
                TestUtil.AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key].RoomPosition, 4);
        }

        [TestMethod]
        public void TestHexMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map, 6, 1);

            Map.Cave = TestUtil.HexTestCave;
            MapRenderer.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(TestUtil.HexExpectedRoomPoints.Count, MapRenderer.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(TestUtil.HexExpectedRoomPoints.Keys.SequenceEqual(MapRenderer.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in TestUtil.HexExpectedRoomPoints)
                TestUtil.AssertVector(Val.Value, MapRenderer.RoomLayout[Val.Key].RoomPosition);
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
                Assert.AreEqual(midpointAngles[i], MapRenderer.GetAngleForSide(i, NumSides), TestUtil.FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                TestUtil.AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapRenderer.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI / NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapRenderer.GetAngleForSectionRadius(i, NumSides), TestUtil.FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                TestUtil.AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapRenderer.GetOffsetForSectionRadius(i, 1, NumSides));
            }

        }

        private static string GetAngleForSectionRadius(int i, int NumSides)
        {
            throw new NotImplementedException();
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
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                midpointAngles[i] = MathUtils.Mod(MathUtils.ToRadians(midpointAngles[i]), Math.PI * 2);
            }
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                //Test side angles
                Assert.AreEqual(midpointAngles[i], MapRenderer.GetAngleForSide(i, NumSides), TestUtil.FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                TestUtil.AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapRenderer.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI / NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapRenderer.GetAngleForSectionRadius(i, NumSides), TestUtil.FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                TestUtil.AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapRenderer.GetOffsetForSectionRadius(i, 1, NumSides));
            }

        }
    }
}
