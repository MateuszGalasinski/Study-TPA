using System.Collections.Generic;

namespace Core.Model
{
    public abstract class BaseAssemblyModel
    {
        public List<BaseNamespaceModel> NamespaceModels { get; set; }

        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            var model = obj as BaseAssemblyModel;
            return model != null &&
                   EqualityComparer<List<BaseNamespaceModel>>.Default.Equals(NamespaceModels, model.NamespaceModels) &&
                   Name == model.Name;
        }
    }
}