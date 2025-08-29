using System;

namespace DynamicClassProj.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormulaAttribute : Attribute
    {
        public string FieldName { get; }
        public FormulaAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}