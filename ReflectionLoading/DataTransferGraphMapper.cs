using System.Collections.Generic;
using System.Linq;
using BaseCore.Model;
using Logic.Models;

namespace ReflectionLoading
{
    public static class DataTransferGraphMapper
    {
        public static AssemblyBase AssemblyBase(AssemblyModel assemblyLogicReader)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblyLogicReader.Name,
                Namespaces = assemblyLogicReader.NamespaceModels?.Select(NamespaceBase).ToList()
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceModel namespaceLogicReader)
        {
            return new NamespaceBase()
            {
                Name = namespaceLogicReader.Name,
                Types = namespaceLogicReader.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeModel TypeModel)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = TypeModel.Name
            };

            _typeDictionary.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = TypeModel.NamespaceName;
            typeBase.Type = TypeModel.Type.ToBaseEnum();
            typeBase.BaseType = GetOrAdd(TypeModel.BaseType);
            typeBase.DeclaringType = GetOrAdd(TypeModel.DeclaringType);
            typeBase.IsAbstract = TypeModel.IsAbstract.ToBaseEnum();
            typeBase.Accessibility = TypeModel.Accessibility.ToBaseEnum();
            typeBase.IsSealed = TypeModel.IsSealed.ToBaseEnum();
            typeBase.IsStatic = TypeModel.IsStatic.ToBaseEnum();

            typeBase.Constructors = TypeModel.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = TypeModel.Fields?.Select(FieldBase).ToList();
            typeBase.GenericArguments = TypeModel.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = TypeModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = TypeModel.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = TypeModel.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = TypeModel.Properties?.Select(PropertyBase).ToList();

            return typeBase;
        }

        public static MethodBase MethodBase(MethodModel MethodModel)
        {
            return new MethodBase()
            {
                Name = MethodModel.Name,
                IsAbstract = MethodModel.IsAbstract.ToBaseEnum(),
                Accessibility = MethodModel.Accessibility.ToBaseEnum(),
                Extension = MethodModel.Extension,
                ReturnType = GetOrAdd(MethodModel.ReturnType),
                IsStatic = MethodModel.IsStatic.ToBaseEnum(),
                VirtualEnum = MethodModel.IsVirtual.ToBaseEnum(),
                GenericArguments = MethodModel.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = MethodModel.Parameters?.Select(ParameterBase).ToList()
            };
        }

        public static ParameterBase ParameterBase(ParameterModel parameterLogicReader)
        {
            return new ParameterBase()
            {
                Name = parameterLogicReader.Name,
                Type = GetOrAdd(parameterLogicReader.Type)
            };
        }

        public static FieldBase FieldBase(FieldModel parameterLogicReader)
        {
            return new FieldBase()
            {
                Name = parameterLogicReader.Name,
                Type = GetOrAdd(parameterLogicReader.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertyModel PropertyModel)
        {
            return new PropertyBase()
            {
                Name = PropertyModel.Name,
                Type = GetOrAdd(PropertyModel.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeModel baseType)
        {
            if (baseType != null)
            {
                if (_typeDictionary.ContainsKey(baseType.Name))
                {
                    return _typeDictionary[baseType.Name];
                }
                else
                {
                    return TypeBase(baseType);
                }
            }
            else
                return null;
        }

        private static Dictionary<string, TypeBase> _typeDictionary;
    }
}
