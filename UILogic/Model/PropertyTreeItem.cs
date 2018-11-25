using ReflectionLoading.Models;

namespace UILogic.Model
{
    public class PropertyTreeItem : TreeItem
    {
        
        public PropertyModel PropertyModel { get; set; }

        public override string TypeName => "Property ";

        public PropertyTreeItem(PropertyModel type, string name)
            : base(name)
        {
            PropertyModel = type;
        }

        protected override void LoadChildrenItems()
        {
            if (PropertyModel.Type != null)
            {
                Children.Add(new TypeTreeItem(GetOrAdd(PropertyModel.Type)));
            }
        }
    }
}
