using System;

namespace DynamicClassProj.Utilities.Test
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }
        public DynamicDisplayNameAttribute(string displayName) => DisplayName = displayName;
    }
}