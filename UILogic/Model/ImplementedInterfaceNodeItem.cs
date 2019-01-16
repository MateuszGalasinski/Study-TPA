using Logic.Enums;
using Logic.Models;
using FieldModel = Logic.Models.FieldModel;
using MethodModel = Logic.Models.MethodModel;
using PropertyModel = Logic.Models.PropertyModel;

namespace UILogic.Model
{
    public class ImplementedInterfaceTreeItem : TreeItem
    {
        private readonly Logic.Models.TypeModel _TypeModel;

        public ImplementedInterfaceTreeItem(Logic.Models.TypeModel TypeModel)
            : base(GetModifiers(TypeModel) + TypeModel.Name)
        {
            _TypeModel = TypeModel;
        }

        public static string GetModifiers(Logic.Models.TypeModel model)
        {
            string type = null;
            type += model.Accessibility == Accessibility.Default ? string.Empty : model.Accessibility.ToString().ToLower() + " ";
            type += model.IsSealed == IsSealed.Sealed ? IsSealed.Sealed.ToString().ToLower() + " " : string.Empty;
            type += model.IsAbstract == IsAbstract.Abstract ? IsAbstract.Abstract.ToString().ToLower() + " " : string.Empty;
            type += model.IsStatic == IsStatic.Static ? IsStatic.Static.ToString().ToLower() + " " : string.Empty;
            return type;
        }

        protected override void LoadChildrenItems()
        {
            if (_TypeModel.BaseType != null)
            {
                ModelHelperMethods.CheckOrAdd(_TypeModel.BaseType);
                Children.Add(new DerivedTypeTreeItem(Logic.Models.TypeModel.TypeDictionary[_TypeModel.BaseType.Name]));
            }

            if (_TypeModel.DeclaringType != null)
            {
                ModelHelperMethods.CheckOrAdd(_TypeModel.DeclaringType);
                Children.Add(new TypeTreeItem(Logic.Models.TypeModel.TypeDictionary[_TypeModel.DeclaringType.Name]));
            }

            if (_TypeModel.Properties != null)
            {
                foreach (PropertyModel PropertyModel in _TypeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(PropertyModel, GetModifiers(PropertyModel.Type) + PropertyModel.Type.Name + " " + PropertyModel.Name));
                }
            }

            if (_TypeModel.Fields != null)
            {
                foreach (FieldModel FieldModel in _TypeModel.Fields)
                {
                    Children.Add(new FieldTreeItem(FieldModel));
                }
            }

            if (_TypeModel.GenericArguments != null)
            {
                foreach (Logic.Models.TypeModel typeModel in _TypeModel.GenericArguments)
                {
                    ModelHelperMethods.CheckOrAdd(typeModel);
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_TypeModel.ImplementedInterfaces != null)
            {
                foreach (Logic.Models.TypeModel typeModel in _TypeModel.ImplementedInterfaces)
                {
                    ModelHelperMethods.CheckOrAdd(typeModel);
                    Children.Add(new ImplementedInterfaceTreeItem(Logic.Models.TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_TypeModel.NestedTypes != null)
            {
                foreach (Logic.Models.TypeModel typeModel in _TypeModel.NestedTypes)
                {
                    ModelHelperMethods.CheckOrAdd(typeModel);
                    Children.Add(new TypeTreeItem(Logic.Models.TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_TypeModel.Methods != null)
            {
                foreach (MethodModel MethodModel in _TypeModel.Methods)
                {
                    Children.Add(new MethodTreeItem(MethodModel));
                }
            }

            if (_TypeModel.Constructors != null)
            {
                foreach (MethodModel MethodModel in _TypeModel.Constructors)
                {
                    Children.Add(new MethodTreeItem(MethodModel));
                }
            }

            if (_TypeModel.Attributes != null)
            {
                foreach (var TypeModel in _TypeModel.Attributes)
                {
                    Children.Add(new TypeTreeItem(TypeModel));
                }
            }
        }
    }
}
