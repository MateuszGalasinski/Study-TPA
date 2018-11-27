using Core.Constants;
using Reflection.Model;
using ReflectionLoading.Models;
using System.Text;

namespace UILogic.Model
{
    public class MethodTreeItem : TreeItem
    {
        public MethodModel MethodModel { get; set; }

        public MethodTreeItem(MethodModel methodModel)
            : base(GetModifiers(methodModel) + methodModel.Name)
        {
            MethodModel = methodModel;
        }

        public static string GetModifiers(MethodModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(model.Accessibility.ToString().ToLower() + " ");
            builder.Append(model.IsAbstract == IsAbstract.Abstract ? IsAbstract.Abstract.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.IsVirtual == IsVirtual.Virtual ? IsVirtual.Virtual.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.IsStatic == IsStatic.Static ? IsStatic.Static.ToString().ToLower() + " " : string.Empty);
            return builder.ToString();
        }

        protected override void LoadChildrenItems()
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(GetOrAdd(genericArgument)));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    Children.Add(new ParameterTreeItem(parameter));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                Children.Add(new TypeTreeItem(GetOrAdd(MethodModel.ReturnType)));
            }
        }
    }
}
