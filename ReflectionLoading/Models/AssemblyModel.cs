using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Core.Model;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "Customer")]
    public class AssemblyModel : BaseAssemblyModel
    {


        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = new List<BaseNamespaceModel>();

            foreach (var type in types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key))
            {
                NamespaceModels.Add(new NamespaceModel(type.Key, type.ToList()));
            }
                
                //= types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                //.Select(t => new NamespaceModel(t.Key, t.ToList())).ToList();

        }

        public override bool Equals(object obj)
        {
            var model = obj as AssemblyModel;
            return model != null &&
                   EqualityComparer<List<BaseNamespaceModel>>.Default.Equals(NamespaceModels, model.NamespaceModels) &&
                   Name == model.Name;
        }
    }
}
