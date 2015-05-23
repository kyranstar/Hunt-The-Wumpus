using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class MiscUtils
    {
        static readonly Random Random = new Random();

        public static T GetRandom<T>(this IEnumerable<T> Me)
        {
            if (Me.Count() <= 0)
                return default(T);
            
            return Me.ElementAt(Me.GetRandomIndex());
        }

        public static int GetRandomIndex<T>(this IEnumerable<T> Me)
        {
            if (Me.Count() <= 0)
                return -1;

            return Random.Next(Me.Count());
        }

        public static int RandomIndex(int Length)
        {
            if (Length <= 0)
                return -1;

            return Random.Next(Length);
        }

        /// <summary>
        /// Rounds and casts the current double value.
        /// Shorthand for <code>(int)Math.Round(Value)</code>
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>The closest integer to the current double value.</returns>
        public static int ToInt(this double Value)
        {
            return (int)Math.Round(Value);
        }

        /// <summary>
        /// Rounds and casts the current float value.
        /// Shorthand for <code>(int)Math.Round(Value)</code>
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>The closest integer to the current float value.</returns>
        public static int ToInt(this float Value)
        {
            return (int)Math.Round(Value);
        }

        public static Rectangle ToRect(this Viewport Viewport, float xScale = 1, float yScale = 1)
        {
            return new Rectangle((Viewport.X * xScale).ToInt(), (Viewport.Y * yScale).ToInt(), Viewport.Width, Viewport.Height);
        }

        public static Vector2 Clone(this Vector2 Source)
        {
            return new Vector2(Source.X, Source.Y);
        }

        /// <summary>
        /// Checks for approximate equality of two vectors within a given threshold.
        /// TODO: I know this name sucks, but I like it ;)
        /// </summary>
        /// <param name="Vector"></param>
        /// <param name="Threshold"></param>
        /// <returns></returns>
        public static bool EqualsIsh(this Vector2 Vector, Vector2 Other, float Threshold = 0.000001f)
        {
            return Math.Abs(Vector.X - Other.X) <= Threshold && Math.Abs(Vector.Y - Other.Y) <= Threshold;
        }

        public static bool ContainsWithinThreshold(this Vector2[] VectorList, Vector2 Target, float Threshold = 0.000001f)
        {
            foreach (Vector2 Vector in VectorList)
                if (Vector.EqualsIsh(Target, Threshold))
                    return true;

            return false;
        }

        public static T RunSynchronouslyWithReturn<T>(this Task<T> Target)
        {
            return Target.Result;
        }

        public static string[] SplitCommaWithGrouping(this string SourceString)
        {
            return Regex.Split(SourceString, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
        }
    }

    public static class ColorUtils
    {
        public static Color FromAlpha(float Alpha = 1)
        {
            return Color.White * Alpha;
        }
    }
}
