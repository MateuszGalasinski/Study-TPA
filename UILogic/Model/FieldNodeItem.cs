using Logic.Models;

namespace UILogic.Model
{
    public class FieldTreeItem : TreeItem
    {
        public FieldModel FieldModel { get; set; }

        public FieldTreeItem(FieldModel fieldModel)
            : base(fieldModel.Name)
        {
            FieldModel = fieldModel;
        }

        protected override void LoadChildrenItems()
        {
            if (FieldModel.Type != null)
            {
                ModelHelperMethods.CheckOrAdd(FieldModel.Type);
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[FieldModel.Type.Name]));
            }
        }
    }
}