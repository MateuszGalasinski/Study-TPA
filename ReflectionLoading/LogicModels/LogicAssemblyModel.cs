using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionLoading.LogicModels
{
    public class LogicAssemblyModel
    {
        public List<LogicNamespaceModel> NamespaceModels { get; set; }

        public string Name { get; set; }

        public LogicAssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new LogicNamespaceModel(t.Key, t.ToList())).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as LogicAssemblyModel;
            return model != null &&
                   EqualityComparer<List<LogicNamespaceModel>>.Default.Equals(NamespaceModels, model.NamespaceModels) &&
                   Name == model.Name;
        }
    }
}
