using ReflectionLoading.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Model;

namespace Reflection.Model
{
    [DataContract(Name = "ParameterModel")]
    public class ParameterModel : BaseParameterModel
    {        

        public ParameterModel(string name, BaseTypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }

        public override bool Equals(object obj)
        {
            var model = obj as ParameterModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(Type, model.Type);
        }
    }
}
