using ReflectionLoading.Models;

namespace UILogic.Model
{
    public class PropertyTreeItem : TreeItem
    {
        
        public PropertyModel PropertyModel { get; set; }

        public PropertyTreeItem(PropertyModel type, string name)
            : base(name)
        {
            PropertyModel = type;
        }

        protected override void LoadChildrenItems()
        {
            if (PropertyModel.Type != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[PropertyModel.Type.Name]));
            }
        }
    }
}
