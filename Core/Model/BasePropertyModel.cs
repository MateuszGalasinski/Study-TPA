using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Model
{
    public abstract class BasePropertyModel
    {
        public string Name { get; set; }
        public BaseTypeModel Type { get; set; }

        public BasePropertyModel(string name, BaseTypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public override bool Equals(object obj)
        {
            var model = obj as BasePropertyModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}