using Reflection.Model;
using ReflectionLoading.Models;

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
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[ParameterModel.Type.Name]));
            }
        }
    }
}
