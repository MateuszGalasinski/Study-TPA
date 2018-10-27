using Core.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Model
{
    internal class PropertyMetadata
    {
        private readonly string _name;
        private readonly TypeMetadata _typeMetadata;

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
          _name = propertyName;
          _typeMetadata = propertyType;
        }

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
          return from prop in props
                 where prop.GetGetMethod().IsVisible() || prop.GetSetMethod().IsVisible()
                 select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }
    }
}