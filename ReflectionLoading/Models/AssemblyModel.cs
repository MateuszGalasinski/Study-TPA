using Core.Model;
using System.Collections.Generic;

namespace ReflectionLoading.Models
{
    public class AssemblyModel : BaseAssemblyModel
    {
        public AssemblyModel(LogicModels.LogicAssemblyModel assemblyModel )
        {
            Name = assemblyModel.Name;
            NamespaceModels = new List<BaseNamespaceModel>();

            foreach (var namespaceModel in assemblyModel.NamespaceModels)
            {
                NamespaceModels.Add(new NamespaceModel(namespaceModel));
            }
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
