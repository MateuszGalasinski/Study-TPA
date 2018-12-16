using System;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionLoading.LogicModels
{
    public class NamespaceModel
    {
        public string Name { get; set; }
        
        public List<TypeModel> Types { get; set; }

        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new TypeModel(t)).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as NamespaceModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<TypeModel>>.Default.Equals(Types, model.Types);
        }
    }
}
