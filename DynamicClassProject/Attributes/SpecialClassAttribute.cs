using System;

namespace DynamicClassProject.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SpecialClassAttribute : Attribute
    {
    }
}