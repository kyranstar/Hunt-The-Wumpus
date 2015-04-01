using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Helpers
{
    static class MathUtils
    {
        public static double PolygonWidth(int NumSides, double Apothem)
        {
            double SideLength = 2d * Math.Tan((Math.PI * 2d)/(2 * NumSides)) * Apothem;
            return Math.Sqrt(2d * Math.Pow(SideLength, 2) - 4 * SideLength * Math.Cos((Math.PI * 2) - (Math.PI * 2)/NumSides));
        }
    }
}
