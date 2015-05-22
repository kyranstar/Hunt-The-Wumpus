using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class PropertyGroupAttribute : Attribute
    {
        public string GroupName { get; set; }

        public PropertyGroupAttribute(string GroupName)
        {
            this.GroupName = GroupName;
        }

        public static string[] GetPropertyNamesByGroup(Type TargetType, string GroupName, int recurseDepth = 1)
        {
            List<string> PropertyNameResults = new List<string>();

            foreach(PropertyInfo Property in TargetType.GetTypeInfo().DeclaredProperties)
            {
                Attribute[] GroupAttributes = GetCustomAttributes(Property, typeof(PropertyGroupAttribute));
                foreach(Attribute GroupAttribute in GroupAttributes)
                    if(GroupAttribute is PropertyGroupAttribute && (GroupAttribute as PropertyGroupAttribute).GroupName == GroupName)
                        PropertyNameResults.Add(Property.Name);

                if (recurseDepth > 0)
                {
                    string[] SubItems = GetPropertyNamesByGroup(Property.PropertyType, GroupName, recurseDepth - 1);
                    PropertyNameResults.AddRange(SubItems.Select(name => Property.Name + "." + name));
                }
            }

            return PropertyNameResults.ToArray();
        }

#if NETFX_CORE
        private static Attribute[] GetCustomAttributes(MemberInfo Member, Type AttributeType)
        {
            return Member.GetCustomAttributes(AttributeType).ToArray();
        }
#endif
    }
}
