using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.Helpers
{
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
}
