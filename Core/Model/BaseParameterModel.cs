using System.Collections.Generic;

namespace Core.Model
{
    public class BaseParameterModel
    {
        public string Name { get; set; }

        public BaseTypeModel Type { get; set; }


        public override bool Equals(object obj)
        {
            var model = obj as BaseParameterModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}