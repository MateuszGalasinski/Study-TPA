using AssemblyReflection.ExtensionMethods;
using Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyReflection.Model
{
    internal class PropertyLoader
    {
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                where prop.GetGetMethod().IsVisible() || prop.GetSetMethod().IsVisible()
                select new PropertyMetadata
                {
                    Name = prop.Name,
                    Metadata = TypeLoader.EmitReference(prop.PropertyType)
                };
        }
    }
}