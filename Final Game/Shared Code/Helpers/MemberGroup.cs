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
        public string PrimaryGroupName { get; set; }
        public string SecondaryGroupName { get; set; }

        public PropertyGroupAttribute(string PrimaryGroupName, string SecondaryGroupName = null)
        {
            this.PrimaryGroupName = PrimaryGroupName;
            this.SecondaryGroupName = SecondaryGroupName;
        }

        public static string[] GetPropertyNamesByGroup(Type TargetType, string PrimaryGroupName, string SecondaryGroupName = null)
        {
            List<string> PropertyNameResults = new List<string>();

            foreach(PropertyInfo Property in TargetType.GetTypeInfo().DeclaredProperties)
            {
                Attribute[] GroupAttributes = GetCustomAttributes(Property, typeof(PropertyGroupAttribute));
                var x = GroupAttributes
                    .Where(GroupAttribute => GroupAttribute is PropertyGroupAttribute)
                    .Select(GroupAttribute => GroupAttribute as PropertyGroupAttribute);
                var AttributesInGroup = GroupAttributes
                    .Where(GroupAttribute => GroupAttribute is PropertyGroupAttribute)
                    .Select(GroupAttribute => GroupAttribute as PropertyGroupAttribute)
                    .Where(GroupAttribute => GroupAttribute.PrimaryGroupName == PrimaryGroupName
                        && (SecondaryGroupName == null
                            || GroupAttribute.SecondaryGroupName == null
                            || GroupAttribute.SecondaryGroupName == SecondaryGroupName));

                if (AttributesInGroup.Count() > 0)
                {
                    PropertyNameResults.Add(Property.Name);

                    string[] SubItems = GetPropertyNamesByGroup(Property.PropertyType, PrimaryGroupName, SecondaryGroupName);
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
