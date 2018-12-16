using System.Collections.Generic;

namespace ReflectionLoading.LogicModels
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

        public override bool Equals(object obj)
        {
            var model = obj as ParameterModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<TypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
