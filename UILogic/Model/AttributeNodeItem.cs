using Logic.Enums;
using Logic.Models;

namespace UILogic.Model
{
    public class AttributeTreeItem : TreeItem
    {
        private readonly TypeModel _TypeModel;

        public AttributeTreeItem(TypeModel TypeModel)
            : base(GetModifiers(TypeModel) + TypeModel.Name)
        {
            _TypeModel = TypeModel;
        }

        public static string GetModifiers(TypeModel model)
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
                Children.Add(new DerivedTypeTreeItem(TypeModel.TypeDictionary[_TypeModel.BaseType.Name]));
            }

            if (_TypeModel.DeclaringType != null)
            {
                ModelHelperMethods.CheckOrAdd(_TypeModel.DeclaringType);
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[_TypeModel.DeclaringType.Name]));
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
                foreach (TypeModel TypeModel in _TypeModel.GenericArguments)
                {
                    ModelHelperMethods.CheckOrAdd(TypeModel);
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[TypeModel.Name]));
                }
            }

            if (_TypeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel TypeModel in _TypeModel.ImplementedInterfaces)
                {
                    ModelHelperMethods.CheckOrAdd(TypeModel);
                    Children.Add(new ImplementedInterfaceTreeItem(TypeModel.TypeDictionary[TypeModel.Name]));
                }
            }

            if (_TypeModel.NestedTypes != null)
            {
                foreach (TypeModel TypeModel in _TypeModel.NestedTypes)
                {
                    ItemTypeEnum type = TypeModel.Type == TypeKind.ClassType ? ItemTypeEnum.NestedClass :
                        TypeModel.Type == TypeKind.StructType ? ItemTypeEnum.NestedStructure :
                        TypeModel.Type == TypeKind.EnumType ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;

                    ModelHelperMethods.CheckOrAdd(TypeModel);
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[TypeModel.Name]));
                }
            }

            if (_TypeModel.Methods != null)
            {
                foreach (MethodModel MethodModel in _TypeModel.Methods)
                {
                    ItemTypeEnum type = MethodModel.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method;
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
