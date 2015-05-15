﻿using HuntTheWumpus.SharedCode.GameMap;
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
            MapRenderer MapRenderer = new MapRenderer(Map);

            Map.Cave = TestUtil.SquareTestCave;
            Map.Cave.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(TestUtil.SquareExpectedRoomPoints.Count, Map.Cave.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(TestUtil.SquareExpectedRoomPoints.Keys.SequenceEqual(Map.Cave.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in TestUtil.SquareExpectedRoomPoints)
                TestUtil.AssertVector(Val.Value, Map.Cave.RoomLayout[Val.Key].PrimaryRoomPosition, 4);
        }

        [TestMethod]
        public void TestHexMapLayout()
        {
            Map Map = new Map();
            // Test calculations using a square room
            MapRenderer MapRenderer = new MapRenderer(Map);

            Map.Cave = TestUtil.HexTestCave;
            Map.Cave.RegenerateLayout();

            // Make sure that there are the expected number of generated values 
            Assert.AreEqual(TestUtil.HexExpectedRoomPoints.Count, Map.Cave.RoomLayout.Count);
            // Quick check to make sure that all the same room IDs were returned
            Assert.IsTrue(TestUtil.HexExpectedRoomPoints.Keys.SequenceEqual(Map.Cave.RoomLayout.Keys.OrderBy(i => i)));
            // Validate each individual vector
            foreach (var Val in TestUtil.HexExpectedRoomPoints)
                TestUtil.AssertVector(Val.Value, Map.Cave.RoomLayout[Val.Key].PrimaryRoomPosition);
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
                midpointAngles[i] = MathUtils.Mod(MathHelper.ToRadians((float)midpointAngles[i]), Math.PI * 2);
            }
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                //Test side angles
                Assert.AreEqual(midpointAngles[i], MapUtils.GetAngleForSide(i, NumSides), TestUtil.FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                TestUtil.AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapUtils.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI / NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapUtils.GetAngleForSectionRadius(i, NumSides), TestUtil.FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                TestUtil.AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapUtils.GetOffsetForSectionRadius(i, 1, NumSides));
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
                midpointAngles[i] = MathUtils.Mod(midpointAngles[i] * (Math.PI / 180), Math.PI * 2);
            }
            for (int i = 0; i < midpointAngles.Length; i++)
            {
                //Test side angles
                Assert.AreEqual(midpointAngles[i], MapUtils.GetAngleForSide(i, NumSides), TestUtil.FloatThreshold);
                // Test side offsets. Negate Y because of XNA coords
                TestUtil.AssertVector(new Vector2((float)Math.Cos(midpointAngles[i]), (float)(-1 * Math.Sin(midpointAngles[i]))), MapUtils.GetOffsetForSide(i, 1, NumSides));

                double cornerAngle = MathUtils.Mod(midpointAngles[i] + Math.PI / NumSides, 2 * Math.PI);

                // Test corner angles
                Assert.AreEqual(cornerAngle, MapUtils.GetAngleForSectionRadius(i, NumSides), TestUtil.FloatThreshold);

                // Test corner offsets. Negate Y because of XNA coords
                double rad = MathUtils.PolygonRadius(NumSides, 1);
                TestUtil.AssertVector(new Vector2((float)(Math.Cos(cornerAngle) * rad), (float)(-1 * rad * Math.Sin(cornerAngle))), MapUtils.GetOffsetForSectionRadius(i, 1, NumSides));
            }

        }
    }
}
