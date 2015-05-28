using System;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class MathUtils
    {
        /// <summary>
        /// Calculates the width of a regular polygon assuming that it has one side horizontal.
        /// </summary>
        /// <param name="NumSides">The number of sides on the polygon</param>
        /// <param name="Apothem">The apothem of the polygon</param>
        /// <returns></returns>
        public static double PolygonWidth(int NumSides, double Apothem)
        {
            double Theta = (Math.Floor(NumSides / 2d) * 2d * Math.PI) / NumSides;
            double Radius = PolygonRadius(NumSides, Apothem);
            return 2d * Math.Sin(Theta / 2) * Radius;
        }

        /// <summary>
        /// Calculates the radius of a regular polygon.
        /// </summary>
        /// <param name="NumSides">The number of sides on the polygon</param>
        /// <param name="Apothem">The apothem of the polygon</param>
        /// <returns></returns>
        public static double PolygonRadius(int NumSides, double Apothem)
        {
            return Apothem * Sec(Math.PI / NumSides);
        }

        public static double PolygonHeight(int NumSides, double Apothem)
        {
            if (IsEven(NumSides))
                return Apothem * 2d;
            double Radius = Apothem * Sec(Math.PI / NumSides);
            return Radius + Apothem;
        }

        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }


        /// <summary>
        /// Converts polar coordinates into XNA-style cartesian coordinates.
        /// </summary>
        /// <param name="Angle">The theta component (rad)</param>
        /// <param name="Radius">The radius component</param>
        /// <returns></returns>
        public static Vector2 PolarToCart(double Angle, double Radius)
        {
            return new Vector2(
                (float)Math.Cos(Angle) * (float)Radius,
                // Negate for XNA coords
                (float)Math.Sin(Angle) * (float)Radius * -1f);
        }

        /// <summary>
        /// Tests whether a point is inside a hexagon.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="center"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool IsInsideHexagon(Vector2 pos, Vector2 center, int width, int height)
        {
            float q2x = Math.Abs(pos.X - center.X);         // transform the test point locally and to quadrant 2
            float q2y = Math.Abs(pos.Y - center.Y);         // transform the test point locally and to quadrant 2
            if (q2x > width*2 || q2y > height) return false;           // bounding test (since q2 is in quadrant 2 only 2 tests are needed)
            return height * 2 * width - height * q2x - 2* width * q2y >= 0;   // finally the dot product can be reduced to this due to the hexagon symmetry
        }

        /// <summary>
        /// Checks if the given number is even.
        /// </summary>
        /// <param name="d">The number.</param>
        /// <returns>A bool indicating whether the given integer is even.</returns>
        public static bool IsEven(int i)
        {
            return i % 2 == 0;
        }

        /// <summary>
        /// Calculates the secant of the given angle.
        /// </summary>
        /// <param name="Angle">The angle, in radians.</param>
        /// <returns></returns>
        public static double Sec(double Angle)
        {
            return 1d / Math.Cos(Angle);
        }

        /// <summary>
        /// C#'s % operator returns the remainder, not the modulus. This method always returns a positive value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double Mod(double x, double m)
        {
            double r = x % m;
            return r < 0 ? r + m : r;
        }

        public static float ClosestMultipleLessThan(float Divisor, float Threshold)
        {
            return (float)Math.Floor(Threshold / Divisor) * Divisor;
        }

        public static float ClosestMultipleGreaterThan(float Divisor, float Threshold)
        {
            return (float)Math.Ceiling(Threshold / Divisor) * Divisor;
        }

        public static double Difference(this Vector2 Me, Vector2 Other)
        {
            return Math.Sqrt(Math.Pow(Other.X - Me.X, 2) + Math.Pow(Other.Y - Me.Y, 2));
        }

        public static double Distance(this Vector2 Me, Vector2 Other)
        {
            return Math.Abs(Me.Difference(Other));
        }

        public static double Scale(this double CurrentValue, double OldMin, double OldMax, double NewMin, double NewMax)
        {
            CurrentValue -= OldMin;
            CurrentValue /= OldMax - OldMin;
            CurrentValue *= NewMax - NewMin;
            return CurrentValue + NewMin;
        }
    }
}
