using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionLoading.LogicModels
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

        public static List<PropertyModel> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => Models.ExtensionMethods.GetVisible(t.GetGetMethod()) || Models.ExtensionMethods.GetVisible(t.GetSetMethod()))
                .Select(t => new PropertyModel(t.Name, TypeModel.EmitReference(t.PropertyType))).ToList(); 
        }

        public override bool Equals(object obj)
        {
            var model = obj as PropertyModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<TypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
