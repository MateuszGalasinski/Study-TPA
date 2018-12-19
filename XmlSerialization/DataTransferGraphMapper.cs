using Base.Model;
using System.Collections.Generic;
using System.Linq;
using XmlSerialization.Model;

namespace XmlSerialization
{
    public static class DataTransferGraphMapper
    {
        public static AssemblyBase AssemblyBase(AssemblySerializationModel assemblySerializationModel)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblySerializationModel.Name,
                Namespaces = assemblySerializationModel.Namespaces?.Select(NamespaceBase).ToList()
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceSerializationModel namespaceSerializationModel)
        {
            return new NamespaceBase()
            {
                Name = namespaceSerializationModel.Name,
                Types = namespaceSerializationModel.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeSerializationModel typeSerializationModel)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeSerializationModel.Name
            };

            _typeDictionary.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeSerializationModel.NamespaceName;
            typeBase.Type = typeSerializationModel.Type;
            typeBase.BaseType = GetOrAdd(typeSerializationModel.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeSerializationModel.DeclaringType);
            typeBase.AbstractEnum = typeSerializationModel.AbstractEnum;
            typeBase.AccessLevel = typeSerializationModel.AccessLevel;
            typeBase.SealedEnum = typeSerializationModel.SealedEnum;
            typeBase.StaticEnum = typeSerializationModel.StaticEnum;

            typeBase.Constructors = typeSerializationModel.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeSerializationModel.Fields?.Select(ParameterBase).ToList();
            typeBase.GenericArguments = typeSerializationModel.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeSerializationModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeSerializationModel.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeSerializationModel.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeSerializationModel.Properties?.Select(PropertyBase).ToList();

            return typeBase;
        }

        public static MethodBase MethodBase(MethodSerializationModel methodSerializationModel)
        {
            return new MethodBase()
            {
                Name = methodSerializationModel.Name,
                AbstractEnum = methodSerializationModel.AbstractEnum,
                AccessLevel = methodSerializationModel.AccessLevel,
                Extension = methodSerializationModel.Extension,
                ReturnType = GetOrAdd(methodSerializationModel.ReturnType),
                StaticEnum = methodSerializationModel.StaticEnum,
                VirtualEnum = methodSerializationModel.VirtualEnum,
                GenericArguments = methodSerializationModel.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = methodSerializationModel.Parameters?.Select(ParameterBase).ToList()
            };
        }

        public static ParameterBase ParameterBase(ParameterSerializationModel parameterSerializationModel)
        {
            return new ParameterBase()
            {
                Name = parameterSerializationModel.Name,
                Type = GetOrAdd(parameterSerializationModel.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertySerializationModel propertySerializationModel)
        {
            return new PropertyBase()
            {
                Name = propertySerializationModel.Name,
                Type = GetOrAdd(propertySerializationModel.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeSerializationModel baseType)
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
