using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MemberGroupAttribute : Attribute
    {
        public string GroupName { get; set; }

        public MemberGroupAttribute(string GroupName)
        {
            this.GroupName = GroupName;
        }

        public static string[] GetMemberNamesByGroup<T>(T Target, string GroupName)
        {
            List<string> MemberNameResults = new List<string>();

            Type TargetType = typeof(T);
            foreach(MemberInfo Member in TargetType.GetTypeInfo().DeclaredMembers)
            {
                // Property getters/setters show up separately as methods
                // as well as in their parent properties
                if (Member is MethodInfo && (Member.Name.StartsWith("get_") || Member.Name.StartsWith("set_")))
                    continue;

                Attribute[] GroupAttributes = GetCustomAttributes(Member, typeof(MemberGroupAttribute));
                foreach(Attribute GroupAttribute in GroupAttributes)
                    if(GroupAttribute is MemberGroupAttribute && (GroupAttribute as MemberGroupAttribute).GroupName == GroupName)
                        MemberNameResults.Add(Member.Name);
            }

            return MemberNameResults.ToArray();
        }

#if NETFX_CORE
        private static Attribute[] GetCustomAttributes(MemberInfo Member, Type AttributeType)
        {
            return Member.GetCustomAttributes(AttributeType).ToArray();
        }
#endif
    }
}
