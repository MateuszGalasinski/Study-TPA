using Core.Model;
using ReflectionLoading.LogicModels;
using System.Collections.Generic;

namespace ReflectionLoading.Models
{
    public class NamespaceModel : BaseNamespaceModel
    {
        public NamespaceModel(LogicNamespaceModel namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = new List<BaseTypeModel>();

            foreach (var type in namespaceModel.Types)
            {
                Types.Add(new TypeModel(type));
            }
        }

        public override bool Equals(object obj)
        {
            var model = obj as NamespaceModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(Types, model.Types);
        }
    }
}
