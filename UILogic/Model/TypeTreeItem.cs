using Core.Constants;
using ReflectionLoading.Models;
using System;
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
            if (model.Modifiers != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(model.Modifiers.Item1.ToString().ToLower() + " ");
                builder.Append(model.Modifiers.Item2 == IsSealed.Sealed ? IsSealed.Sealed.ToString().ToLower() + " " : string.Empty);
                builder.Append(model.Modifiers.Item3 == IsAbstract.Abstract ? IsAbstract.Abstract.ToString().ToLower() + " " : string.Empty);
                builder.Append(model.Modifiers.Item4 == IsStatic.Static ? IsStatic.Static.ToString().ToLower() + " " : string.Empty);
                return builder.ToString();
            }

            return null;
        }

        protected override void LoadChildrenItems()
        {
            if (_typeModel.BaseType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[_typeModel.BaseType.Name]));
            }

            if (_typeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[_typeModel.DeclaringType.Name]));
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
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_typeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in _typeModel.ImplementedInterfaces)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_typeModel.NestedTypes != null)
            {
                foreach (TypeModel typeModel in _typeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
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
