using ReflectionLoading.Models;


namespace UILogic.Model
{
    public class NamespaceTreeItem : TreeItem
    {
        private readonly NamespaceModel _namespaceModel;

        public NamespaceTreeItem(NamespaceModel namespaceModel)
            : base(namespaceModel.Name)
        {
            _namespaceModel = namespaceModel;
        }

        protected override void LoadChildrenItems()
        {
            if (_namespaceModel?.Types != null)
            {
                foreach (TypeModel typeModel in _namespaceModel?.Types)
                {
                    Children.Add(new TypeTreeItem(GetOrAdd(typeModel)));
                }
            }
        }
    }
}
