using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using HuntTheWumpus.SharedCode.GameMap;
using System.Reflection;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GameControl
{
    public static class ArgumentParser
    {

        // TODO: Be cognizant of quotes
        private const string ArgPattern = @"(?<PlainParameter_Value>\w+)|\/(?<NamedParameter_Name>\w*)[ ](?<NamedParameter_Value>\w+)|\/(?<BooleanFlag_Name>\w*)";

        public static Argument[] Parse(string ArgumentList)
        {
            Regex ArgRegex = new Regex(ArgPattern, RegexOptions.ExplicitCapture);

            ArgumentGroupID[] Groups = ArgRegex.GetGroupNames().Skip(1).Select(Name => 
                {
                    string[] NameParts = Name.Split('_');
                    Argument.ArgumentType Type = (Argument.ArgumentType)Enum.Parse(typeof(Argument.ArgumentType), NameParts[0]);
                    return new ArgumentGroupID() { ArgumentType = Type, PropertyName = NameParts[1] };
                }).ToArray();

            return ArgRegex.Matches(ArgumentList).Cast<Match>().Select(m => ConvertMatchToArgument(Groups, m)).ToArray();
        }

        private static Argument ConvertMatchToArgument(ArgumentGroupID[] Groups, Match Match)
        {
            Argument.ArgumentType ArgumentType = Argument.ArgumentType.Unknown;
            foreach(ArgumentGroupID Group in Groups)
                if(Match.Groups[Group.GroupName].Success)
                    ArgumentType = Group.ArgumentType;

            var TypeGroups = Groups.Where(Group => Group.ArgumentType == ArgumentType);

            Argument Result = new Argument() { Type = ArgumentType };
            foreach (ArgumentGroupID Group in TypeGroups)
                ReflectionUtils.SetProperty<Argument>(Result, Group.PropertyName, Match.Groups[Group.GroupName].Value);

            return Result;
        }

        public static string GetParam(Argument[] Args, params string[] Keys)
        {
            Argument Val = Args.FirstOrDefault(Arg => Keys.Contains(Arg.Name));
            if (Val == null)
                return null;
            return Val.Value;
        }
    }

    public class ArgumentGroupID
    {
        public Argument.ArgumentType ArgumentType { get; set; }
        public string PropertyName { get; set; }
        public string GroupName
        {
            get
            {
                return ArgumentType.ToString() + "_" + PropertyName;
            }
        }

        public override string ToString()
        {
            return GroupName;
        }


    }

    public class Argument
    {
        public enum ArgumentType
        {
            
            Unknown,
            PlainParameter,
            BooleanFlag,
            NamedParameter
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public ArgumentType Type {get; set; }
    }
}