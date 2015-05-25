using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HuntTheWumpus.SharedCode.GameControl
{
    /// <summary>
    /// Provides helpers to parse command-line
    /// argument strings and extract flags,
    /// named parameters, and loose data.
    /// </summary>
    public static class ArgumentParser
    {

        // TODO: Be cognizant of quotes
        /// <summary>
        /// The regex pattern that will separate the useful
        /// parts of the argument strings. This uses named groups
        /// to document all of the required data.
        /// 
        /// The group names are split into two parts. The
        /// first is the type of parameter that it will capture
        /// data for -- PlainParameter, NamedParameter, or
        /// BooleanFlag. The second part is the property that it
        /// sets on an Argument object. These are reflected
        /// directly onto the result.
        /// </summary>
        private const string ArgPattern = @"(?<PlainParameter_Value>\w+)|\/(?<NamedParameter_Name>\w*)[ ](?<NamedParameter_Value>\w+)|\/(?<BooleanFlag_Name>\w*)";

        /// <summary>
        /// Parses the given argument string.
        /// </summary>
        /// <param name="ArgumentList">The argument string to be parsed.</param>
        /// <returns>An array of argument objects</returns>
        public static Argument[] Parse(string ArgumentList)
        {
            // Create the regex object for use in parsing args
            Regex ArgRegex = new Regex(ArgPattern, RegexOptions.ExplicitCapture);

            // Get the groups from the regex
            ArgumentGroupID[] Groups = ArgRegex.GetGroupNames().Skip(1).Select(Name => 
                {
                    // Split by underscores
                    string[] NameParts = Name.Split('_');

                    // Parse the type for the argument
                    Argument.ArgumentType Type = (Argument.ArgumentType)Enum.Parse(typeof(Argument.ArgumentType), NameParts[0]);

                    // Construct a result
                    return new ArgumentGroupID() { ArgumentType = Type, PropertyName = NameParts[1] };
                }).ToArray();

            return ArgRegex.Matches(ArgumentList).Cast<Match>().Select(m => ConvertMatchToArgument(Groups, m)).ToArray();
        }

        /// <summary>
        /// Converts the given group results and match
        /// into an Argument object.
        /// </summary>
        /// <param name="Groups">The group descriptors that were used to get the given match.</param>
        /// <param name="Match">The match to process.</param>
        /// <returns></returns>
        private static Argument ConvertMatchToArgument(ArgumentGroupID[] Groups, Match Match)
        {
            // Find the argument type that was matched
            Argument.ArgumentType ArgumentType = Argument.ArgumentType.Unknown;
            foreach(ArgumentGroupID Group in Groups)
                if(Match.Groups[Group.GroupName].Success)
                    ArgumentType = Group.ArgumentType;

            // Isolate the groups that were matched
            var TypeGroups = Groups.Where(Group => Group.ArgumentType == ArgumentType);

            // Create an object to be returned
            Argument Result = new Argument() { Type = ArgumentType };
            
            // Set all of the properties from the group name
            foreach (ArgumentGroupID Group in TypeGroups)
                ReflectionUtils.SetProperty(Result, Group.PropertyName, Match.Groups[Group.GroupName].Value);

            // Return the result
            return Result;
        }

        /// <summary>
        /// Returns the value that was captured
        /// by the specified named parameter,
        /// or null if the parameter could
        /// not be found.
        /// </summary>
        /// <param name="Args">The argument set to search</param>
        /// <param name="Keys">The target keys to look for</param>
        /// <returns></returns>
        public static string GetParam(Argument[] Args, params string[] Keys)
        {
            Argument Val = Args.FirstOrDefault(Arg => Keys.Contains(Arg.Name));
            if (Val == null)
                return null;
            return Val.Value;
        }
    }

    /// <summary>
    /// Describes the target argument
    /// of a capture group
    /// </summary>
    public class ArgumentGroupID
    {
        /// <summary>
        /// The type of the target argument
        /// </summary>
        public Argument.ArgumentType ArgumentType { get; set; }

        /// <summary>
        /// The name of the property
        /// on the argument that this
        /// group sets
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The group name as it
        /// appeared in the regex
        /// </summary>
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

    /// <summary>
    /// A single argument that was
    /// passed as a command-line arg.
    /// </summary>
    public class Argument
    {
        public enum ArgumentType
        {
            
            Unknown,
            PlainParameter,
            BooleanFlag,
            NamedParameter
        }

        /// <summary>
        /// The name of the argument,
        /// if this was a named parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of this argument
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The type of this argument
        /// </summary>
        public ArgumentType Type {get; set; }
    }
}