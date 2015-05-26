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

        public static string[] GetPropertyNamesByGroup(Type TargetType, string GroupName)
        {
            List<string> PropertyNameResults = new List<string>();

            foreach(PropertyInfo Property in TargetType.GetTypeInfo().DeclaredProperties)
            {
                Attribute[] GroupAttributes = GetCustomAttributes(Property, typeof(PropertyGroupAttribute));
                var AttributesInGroup = GroupAttributes
                    .Where(GroupAttribute => GroupAttribute is PropertyGroupAttribute)
                    .Select(GroupAttribute => GroupAttribute as PropertyGroupAttribute)
                    .Where(GroupAttribute => GroupAttribute.GroupName == GroupName);

                if (AttributesInGroup.Count() > 0)
                {
                    PropertyNameResults.Add(Property.Name);

                    string[] SubItems = GetPropertyNamesByGroup(Property.PropertyType, GroupName);
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
