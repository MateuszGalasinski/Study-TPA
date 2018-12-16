using System;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionLoading.LogicModels
{
    public class LogicNamespaceModel
    {
        public string Name { get; set; }
        
        public List<LogicTypeModel> Types { get; set; }

        public LogicNamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new LogicTypeModel(t)).ToList();
        }

        public LogicNamespaceModel(LogicModels.LogicNamespaceModel namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = new List<LogicTypeModel>();
            foreach (var type in namespaceModel.Types)
            {
                Types.Add(new LogicTypeModel(type));
            }
        }

        public override bool Equals(object obj)
        {
            var model = obj as LogicNamespaceModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<LogicTypeModel>>.Default.Equals(Types, model.Types);
        }
    }
}
