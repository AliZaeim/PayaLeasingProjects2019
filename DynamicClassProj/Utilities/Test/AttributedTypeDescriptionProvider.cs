using System;
using System.ComponentModel;

namespace DynamicClassProj.Utilities.Test
{
    public class AttributedTypeDescriptionProvider : TypeDescriptionProvider
    {
        public AttributedTypeDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(MyClass))) { }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor baseDescriptor = base.GetTypeDescriptor(objectType, instance);
            return new AttributedCustomTypeDescriptor(baseDescriptor);
        }
    }
}