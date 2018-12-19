﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "ParameterModel")]
    public class ParameterModel
    {        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
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
