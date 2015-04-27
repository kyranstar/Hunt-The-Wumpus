using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("The target property does not exist.");

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
                throw new Exception("The target field does not exist.");

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
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetRuntimeField(Name);
            PropertyInfo PropInfo = TargetObjType.GetRuntimeProperty(Name);

            if(FieldInfo != null)
                return FieldInfo.GetValue(Object);
            else if(PropInfo != null)
                return PropInfo.GetValue(Object);
            else
                throw new Exception("The target does not exist.");
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

}
