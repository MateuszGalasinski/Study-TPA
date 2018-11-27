using ReflectionLoading.Models;
using System.Collections.Generic;

namespace Reflection.Model
{
    public class ParameterModel
    {
        
        public string Name { get; set; }
        
        public TypeModel Type { get; set; }

        public ParameterModel(string name, TypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }

        private ParameterModel()
        {

        }

        public override bool Equals(object obj)
        {
            var model = obj as ParameterModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<TypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
