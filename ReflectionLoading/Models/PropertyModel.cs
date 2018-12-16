using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Core.Model;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "PropertyModel")]
    public class PropertyModel : BasePropertyModel
    {   

        public PropertyModel(string name, BaseTypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public static List<BasePropertyModel> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();
            List<BasePropertyModel> propertyModels = new List<BasePropertyModel>();
            foreach (var propertyInfo in props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible()))
            {
                propertyModels.Add(new PropertyModel(propertyInfo.Name, TypeModel.EmitReference(propertyInfo.PropertyType)));
            }

            return propertyModels;
            //return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
            //    .Select(t => new PropertyModel(t.Name, TypeModel.EmitReference(t.PropertyType))).ToList(); 
        }

        public override bool Equals(object obj)
        {
            var model = obj as PropertyModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
