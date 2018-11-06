using System.Collections.Generic;
using System.Reflection;
using AssemblyReflection.Extensions;
using Core.Model;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector
    {
        internal IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props, AssemblyMetadataStore metaStore)
        {
            List<PropertyMetadata> properties = new List<PropertyMetadata>();
            foreach (PropertyInfo property in props)
            {
                if (property.GetGetMethod().IsVisible() || property.GetSetMethod().IsVisible())
                {
                    string id = $"{property.DeclaringType.FullName}.{property.Name}";
                    if (metaStore.PropertiesDictionary.ContainsKey(id))
                    {
                        _logger.Trace("Using property already added to dictionary: Id =" + id);
                        properties.Add(metaStore.PropertiesDictionary[id]);
                    }
                    else
                    {
                        PropertyMetadata newProperty = new PropertyMetadata()
                        {
                            Id = id,
                            Name = property.Name
                        };
                        _logger.Trace("Adding new property to dictionary: " + newProperty.Id +" ;Name = " + newProperty.Name);
                        metaStore.PropertiesDictionary.Add(newProperty.Id, newProperty);
                        properties.Add(newProperty);

                        newProperty.TypeMetadata = LoadTypeMetadataDto(property.PropertyType, metaStore);
                    }
                }
            }

            return properties;
        }
    }
}