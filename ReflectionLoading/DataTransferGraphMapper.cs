using BaseCore.Model;
using Logic.Models;
using System.Collections.Generic;
using System.Linq;

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

        public static TypeBase TypeBase(TypeModel typeLogicReader)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeLogicReader.Name
            };

            _typeDictionary.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeLogicReader.NamespaceName;
            typeBase.Type = typeLogicReader.Type.ToBaseEnum();
            typeBase.BaseType = GetOrAdd(typeLogicReader.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeLogicReader.DeclaringType);
            typeBase.AbstractEnum = typeLogicReader.IsAbstract.ToBaseEnum();
            typeBase.AccessLevel = typeLogicReader.Accessibility.ToBaseEnum();
            typeBase.SealedEnum = typeLogicReader.IsSealed.ToBaseEnum();
            typeBase.StaticEnum = typeLogicReader.IsStatic.ToBaseEnum();

            typeBase.Constructors = typeLogicReader.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeLogicReader.Fields?.Select(ParameterBase).ToList();
            typeBase.GenericArguments = typeLogicReader.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeLogicReader.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeLogicReader.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeLogicReader.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeLogicReader.Properties?.Select(PropertyBase).ToList();

            return typeBase;
        }

        public static MethodBase MethodBase(MethodModel methodLogicReader)
        {
            return new MethodBase()
            {
                Name = methodLogicReader.Name,
                AbstractEnum = methodLogicReader.IsAbstract.ToBaseEnum(),
                AccessLevel = methodLogicReader.Accessibility.ToBaseEnum(),
                Extension = methodLogicReader.Extension,
                ReturnType = GetOrAdd(methodLogicReader.ReturnType),
                StaticEnum = methodLogicReader.IsStatic.ToBaseEnum(),
                VirtualEnum = methodLogicReader.IsVirtual.ToBaseEnum(),
                GenericArguments = methodLogicReader.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = methodLogicReader.Parameters?.Select(ParameterBase).ToList()
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

        public static PropertyBase PropertyBase(PropertyModel propertyLogicReader)
        {
            return new PropertyBase()
            {
                Name = propertyLogicReader.Name,
                Type = GetOrAdd(propertyLogicReader.Type)
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
