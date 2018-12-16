using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public abstract class BaseNamespaceModel
    {
       
        public string Name { get; set; }

        public List<BaseTypeModel> Types { get; set; }


        public override bool Equals(object obj)
        {
            var model = obj as BaseNamespaceModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(Types, model.Types);
        }
    }
}