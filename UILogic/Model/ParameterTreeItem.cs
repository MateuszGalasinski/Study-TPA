using Reflection.Model;

namespace UILogic.Model
{
    public class ParameterTreeItem : TreeItem
    {
        public ParameterModel ParameterModel { get; set; }

        public ParameterTreeItem(ParameterModel parameterModel)
            : base(parameterModel.Name)
        {
            ParameterModel = parameterModel;
        }

        protected override void LoadChildrenItems()
        {
            if (ParameterModel.Type != null)
            {
                Children.Add(new TypeTreeItem(GetOrAdd(ParameterModel.Type)));
            }
        }
    }
}
