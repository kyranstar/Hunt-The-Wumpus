using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Helpers
{
    static class MathUtils
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
            else
            {
                double Radius = Apothem * Sec(Math.PI / NumSides);
                return Radius + Apothem;
            }
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
    }
}
