using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DynamicClassProj.Utilities.Test
{
    public class AttributedCustomTypeDescriptor : CustomTypeDescriptor
    {
        public AttributedCustomTypeDescriptor(ICustomTypeDescriptor parent) : base(parent) { }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection original = base.GetProperties();
            var properties = new PropertyDescriptor[original.Count];

            for (int i = 0; i < original.Count; i++)
            {
                var originalProp = original[i];
                var attributes = new List<Attribute>(originalProp.Attributes.Cast<Attribute>());

                // Dynamically add attribute to the "Name" property
                if (originalProp.Name == "Name")
                    attributes.Add(new DynamicDisplayNameAttribute("Full Name"));

                properties[i] = TypeDescriptor.CreateProperty(
                    typeof(MyClass),
                    originalProp,
                    attributes.ToArray()
                );
            }
            return new PropertyDescriptorCollection(properties);
        }
    }
}