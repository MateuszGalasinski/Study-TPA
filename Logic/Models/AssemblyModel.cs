using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "Customer")]
    public class AssemblyModel
    {
        [DataMember]
        public List<NamespaceModel> NamespaceModels { get; set; }

        [DataMember]
        public string Name { get; set; }

        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList())).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as AssemblyModel;
            return model != null &&
                   EqualityComparer<List<NamespaceModel>>.Default.Equals(NamespaceModels, model.NamespaceModels) &&
                   Name == model.Name;
        }
    }
}
