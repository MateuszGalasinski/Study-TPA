using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionLoading.LogicModels
{
    public class LogicPropertyModel
    {   
        public string Name { get; set; }

        public LogicTypeModel Type { get; set; }

        public LogicPropertyModel(string name, LogicTypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public static List<LogicPropertyModel> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => Models.ExtensionMethods.GetVisible(t.GetGetMethod()) || Models.ExtensionMethods.GetVisible(t.GetSetMethod()))
                .Select(t => new LogicPropertyModel(t.Name, LogicTypeModel.EmitReference(t.PropertyType))).ToList(); 
        }

        public override bool Equals(object obj)
        {
            var model = obj as LogicPropertyModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<LogicTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
