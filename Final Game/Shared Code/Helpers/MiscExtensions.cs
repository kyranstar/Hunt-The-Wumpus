using System;
using System.Collections.Generic;
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
            PropertyInfo PropInfo = TargetObjType.GetProperty(PropName);
            if (PropInfo == null)
                throw new Exception("The target property does not exist.");

            return PropInfo.GetValue(Object);
        }

        public static void SetProperty<T>(T Object, string PropName, object Value)
        {
            Type TargetObjType = typeof(T);
            PropertyInfo PropInfo = TargetObjType.GetProperty(PropName);
            if (PropInfo == null)
                throw new Exception("The target property does not exist.");

            PropInfo.SetValue(Object, Value);
        }

        public static object GetField<T>(T Object, string FieldName)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetField(FieldName);
            if (FieldInfo == null)
                throw new Exception("The target field does not exist.");

            return FieldInfo.GetValue(Object);
        }

        public static void SetField<T>(T Object, string FieldName, object Value)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetField(FieldName);
            if (FieldInfo == null)
                throw new Exception("The target field does not exist.");

            FieldInfo.SetValue(Object, Value);
        }

        public static object GetPropertyOrField<T>(T Object, string Name)
        {
            Type TargetObjType = typeof(T);
            FieldInfo FieldInfo = TargetObjType.GetField(Name);
            PropertyInfo PropInfo = TargetObjType.GetProperty(Name);

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
            FieldInfo FieldInfo = TargetObjType.GetField(Name);
            PropertyInfo PropInfo = TargetObjType.GetProperty(Name);

            if (FieldInfo != null)
                FieldInfo.SetValue(Object, Value);
            else if (PropInfo != null)
                PropInfo.SetValue(Object, Value);
            else
                throw new Exception("The target does not exist.");
        }
    }
}
