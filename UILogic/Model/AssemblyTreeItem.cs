using ReflectionLoading.Models;

namespace UILogic.Model
{
    public class AssemblyTreeItem : TreeItem
    {
        private readonly AssemblyModel _assemblyModel;

        public override string TypeName => "Assembly";

        public AssemblyTreeItem(AssemblyModel assembly)
            : base(assembly.Name)
        {
            _assemblyModel = assembly;
        }

        protected override void LoadChildrenItems()
        {
            if (_assemblyModel?.NamespaceModels != null)
            {
                foreach (NamespaceModel namespaceModel in _assemblyModel.NamespaceModels)
                {
                    Children.Add(new NamespaceTreeItem(namespaceModel));
                }
            }
        }
    }
}
