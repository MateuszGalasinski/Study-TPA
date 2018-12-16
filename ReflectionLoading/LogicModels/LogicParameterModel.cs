using System.Collections.Generic;

namespace ReflectionLoading.LogicModels
{
    public class LogicParameterModel
    {        
        public string Name { get; set; }

        public LogicTypeModel Type { get; set; }

        public LogicParameterModel(string name, LogicTypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }

        public override bool Equals(object obj)
        {
            var model = obj as LogicParameterModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<LogicTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
