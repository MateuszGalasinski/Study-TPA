using Core.Constants;
using Reflection.Model;
using ReflectionLoading.Models;
using System.Text;

namespace UILogic.Model
{
    public class TypeTreeItem : TreeItem
    {
        private readonly TypeModel _typeModel;

        public TypeTreeItem(TypeModel typeModel) : base(GetModifiers(typeModel) + typeModel.Name)
        {
           _typeModel = typeModel;
        }

        public static string GetModifiers(TypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(model.Accessibility.ToString().ToLower() + " ");
            builder.Append(model.IsSealed == IsSealed.Sealed ? IsSealed.Sealed.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.IsAbstract == IsAbstract.Abstract ? IsAbstract.Abstract.ToString().ToLower() + " " : string.Empty);
            builder.Append(model.IsStatic == IsStatic.Static ? IsStatic.Static.ToString().ToLower() + " " : string.Empty);
            return builder.ToString();
        }

        protected override void LoadChildrenItems()
        {
            if (_typeModel.BaseType != null)
            {
                Children.Add(new TypeTreeItem(GetOrAdd(_typeModel.BaseType)));
            }

            if (_typeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem(GetOrAdd(_typeModel.DeclaringType)));
            }

            if (_typeModel.Properties != null)
            {
                foreach (PropertyModel propertyModel in _typeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(propertyModel, GetModifiers(propertyModel.Type) + propertyModel.Type.Name + " " + propertyModel.Name));
                }
            }

            if (_typeModel.Fields != null)
            {
                foreach (ParameterModel parameterModel in _typeModel.Fields)
                {
                    Children.Add(new ParameterTreeItem(parameterModel));
                }
            }

            if (_typeModel.GenericArguments != null)
            {
                foreach (TypeModel typeModel in _typeModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(GetOrAdd(typeModel)));
                }
            }

            if (_typeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in _typeModel.ImplementedInterfaces)
                {
                    Children.Add(new TypeTreeItem(GetOrAdd(typeModel)));
                }
            }

            if (_typeModel.NestedTypes != null)
            {
                foreach (TypeModel typeModel in _typeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(GetOrAdd(typeModel)));
                }
            }

            if (_typeModel.Methods != null)
            {
                foreach (MethodModel methodModel in _typeModel.Methods)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }

            if (_typeModel.Constructors != null)
            {
                foreach (MethodModel methodModel in _typeModel.Constructors)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }
        }
    }
}
