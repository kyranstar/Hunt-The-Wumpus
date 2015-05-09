using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns a new dictionary of this ... others merged leftward.
        /// Keeps the type of 'this', which must be default-instantiable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="me"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static T MergeLeft<T, K, V>(this T me, params IDictionary<K, V>[] others)
            where T : IDictionary<K, V>, new()
        {
            T newMap = new T();
            foreach (IDictionary<K, V> src in
                (new List<IDictionary<K, V>> { me }).Concat(others))
            {
                foreach (KeyValuePair<K, V> p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }

    }

    public static class ReflectionUtils
    {
        public static object GetProperty<T>(T Object, string PropName)
        {
            Type TargetObjType = typeof(T);
            PropertyInfo PropInfo = TargetObjType.GetRuntimeProperty(PropName);
            if (PropInfo == null)
                return null;

            return PropInfo.GetValue(Object);
        }

        public static void SetProperty<T>(T Object, string PropName, object Value)
        {
            Type TargetObjType = typeof(T);
            PropertyInfo PropInfo = TargetObjType.GetRuntimeProperty(PropName);
            if (PropInfo == null)
                throw new Exception("The target property does not exist.");

            PropInfo.SetValue(Object, Value);
        }

        public static object GetField<T>(T Object, string FieldName)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetRuntimeField(FieldName);
            if (FieldInfo == null)
                return null;

            return FieldInfo.GetValue(Object);
        }

        public static void SetField<T>(T Object, string FieldName, object Value)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetRuntimeField(FieldName);
            if (FieldInfo == null)
                throw new Exception("The target field does not exist.");

            FieldInfo.SetValue(Object, Value);
        }

        public static object GetPropertyOrField<T>(T Object, string Name)
        {
            object FieldResult = GetField(Object, Name);
            object PropResult = GetProperty(Object, Name);

            return FieldResult ?? PropResult;
        }

        public static void SetPropertyOrField<T>(T Object, string Name, object Value)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetRuntimeField(Name);
            PropertyInfo PropInfo = TargetObjType.GetRuntimeProperty(Name);

            if (FieldInfo != null)
                FieldInfo.SetValue(Object, Convert.ChangeType(Value, FieldInfo.FieldType));
            else if (PropInfo != null)
                PropInfo.SetValue(Object, Convert.ChangeType(Value, PropInfo.PropertyType));
            else
                throw new Exception("The target does not exist.");
        }

#if DESKTOP
        /// <summary>
        /// Returns the class at the given index in the call stack.
        /// </summary>
        /// <param name="framesUp">The number of stack frames to go up. Defaults to one, the direct caller.</param>
        /// <returns>The class info from the target frame</returns>
        public static Type GetCallerClass(int framesUp = 1)
        {
            return GetCallerMethod(framesUp + 1).DeclaringType;
        }

        /// <summary>
        /// Returns the method at the given index in the call stack.
        /// </summary>
        /// <param name="framesUp">The number of stack frames to go up. Defaults to one, the direct caller.</param>
        /// <returns>The method info from the target frame</returns>
        public static MethodBase GetCallerMethod(int framesUp = 1)
        {
            StackFrame frame = new StackFrame(framesUp + 1);
            return frame.GetMethod();
        }

        /// <summary>
        /// Returns the name of the property getter/setter at the given index in the call stack.
        /// </summary>
        /// <param name="framesUp">The number of stack frames to go up. Defaults to one, the direct caller.</param>
        /// <returns>The property name of the target frame</returns>
        public static string GetCallerProperty(int FramesUp = 1)
        {
            // TODO: replace hacks
            return GetCallerMethod(FramesUp + 1).Name.Replace("get_", "").Replace("set_", "");
        }
#endif
    }

    public static class EnumerationExtensions
    {

        /// <summary>
        /// Checks if the given enum has a certain flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the given enum is only the specified flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a flag to the specified enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        /// <summary>
        /// Removes a flag from the specified enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

    }

    public static class MiscUtils
    {
        static Random Random = new Random();

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

        public static Rectangle ToRect(this Viewport Viewport)
        {
            return new Rectangle(Viewport.X, Viewport.Y, Viewport.Width, Viewport.Height);
        }
    }

    public static class ColorUtils
    {
        public static Color FromAlpha(float Alpha = 255)
        {
            return Color.White * Alpha;
        }
    }
}
