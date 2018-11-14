using Core.Constants;
using Reflection.Model;
using ReflectionLoading.Models;
using System.Text;

namespace UILogic.Model
{
    public class MethodTreeItem : TreeItem
    {
        public MethodModel MethodModel { get; set; }

        public override string TypeName => "Method";

        public MethodTreeItem(MethodModel methodModel)
            : base(GetModifiers(methodModel) + methodModel.Name)
        {
            MethodModel = methodModel;
        }

        public static string GetModifiers(MethodModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(model.Modifiers.Item1.ToString().ToLower() + " ");
            builder.Append(model.Modifiers.Item2 == IsAbstract.Abstract ? IsAbstract.Abstract.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.Modifiers.Item4 == IsVirtual.Virtual ? IsVirtual.Virtual.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.Modifiers.Item3 == IsStatic.Static ? IsStatic.Static.ToString().ToLower() + " " : string.Empty);
            return builder.ToString();
        }

        protected override void LoadChildrenItems()
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[genericArgument.Name]));
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
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[MethodModel.ReturnType.Name]));
            }
        }
    }
}
