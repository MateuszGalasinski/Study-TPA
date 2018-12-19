using BaseCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logic.Models
{
    public class PropertyModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public PropertyModel(string name, TypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public PropertyModel(PropertyBase baseProperty)
        {
            Name = baseProperty.Name;
            Type = TypeModel.GetOrAdd(baseProperty.Type);
        }

        public static List<PropertyModel> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyModel(t.Name, TypeModel.GetOrAdd(t.PropertyType))).ToList();
        }
    }
}