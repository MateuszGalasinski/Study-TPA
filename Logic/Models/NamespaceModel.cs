using System;
using System.Collections.Generic;
using System.Linq;
using BaseCore.Model;

namespace Logic.Models
{
    public class NamespaceModel
    {

        public string Name { get; set; }

        public List<TypeModel> Types { get; set; }

        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => TypeModel.GetOrAdd(t)).ToList();
        }

        public NamespaceModel(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            this.Types = namespaceBase.Types?.Select(t => TypeModel.GetOrAdd(t)).ToList();
        }
    }
}