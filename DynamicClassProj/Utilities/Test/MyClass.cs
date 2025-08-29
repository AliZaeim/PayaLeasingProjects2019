using System.ComponentModel;

namespace DynamicClassProj.Utilities.Test
{
    [TypeDescriptionProvider(typeof(AttributedTypeDescriptionProvider))]
    public class MyClass
    {
        public string Name { get; set; } // Will appear as "Full Name"
        public int Age { get; set; }     // Unchanged
    }
}