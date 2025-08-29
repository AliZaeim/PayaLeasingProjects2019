using System;

namespace DynamicClassProj.Utilities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SpecialClassAttribute:Attribute
    {
    }
}