using AssemblyReflection.ExtensionMethods;
using Core.Constants;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyReflection.Model
{
    internal static class TypeLoader
    {
        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return LoadTypeMetadata(type.Name, type.Namespace);
            }
            else
            {
                return LoadTypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type argument in arguments select EmitReference(argument);
        }

        internal static TypeMetadata LoadTypeMetadata(Type type, AssemblyMetadataStore metaStore)
        {
            if (!metaStore.TypesDictionary.ContainsKey(type.FullName))
            {
                TypeMetadata typeMetadata = new TypeMetadata()
                {
                    Name = type.Name,
                    NamespaceName = type.Namespace,
                    Modifiers = EmitModifiers(type),
                    Kind = GetTypeKind(type),
                    Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
                };

                metaStore.TypesDictionary.Add(type.FullName, typeMetadata);

                typeMetadata.DeclaringType = EmitDeclaringType(type.DeclaringType);
                typeMetadata.Constructors = MethodLoader.EmitMethods(type.GetConstructors(), metaStore);
                typeMetadata.Methods = MethodLoader.EmitMethods(type.GetMethods(), metaStore);
                typeMetadata.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), metaStore);
                typeMetadata.ImplementedInterfaces = EmitImplements(type.GetInterfaces());
                typeMetadata.GenericArguments = !type.IsGenericTypeDefinition ? null : TypeLoader.EmitGenericArguments(type.GetGenericArguments());
                typeMetadata.BaseType = EmitExtends(type.BaseType);
                typeMetadata.Properties = PropertyLoader.EmitProperties(type.GetProperties());

                return typeMetadata;
            }
            else
            {
                return metaStore.TypesDictionary[type.FullName];
            }
        }

        private static TypeMetadata LoadTypeMetadata(string typeName, string namespaceName)
        {
            return new TypeMetadata()
            {
                Name = typeName,
                NamespaceName = namespaceName
            };
        }

        private static TypeMetadata LoadTypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments)
        {
            TypeMetadata typeMetadata = LoadTypeMetadata(typeName, namespaceName);
            typeMetadata.GenericArguments = genericArguments;
            return typeMetadata;
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
            {
                return null;
            }

            return EmitReference(baseType);
        }

        private static TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
            {
                return null;
            }

            return EmitReference(declaringType);
        }

        private static IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes, AssemblyMetadataStore metaStore)
        {
            return from type in nestedTypes
                   where type.IsVisible()
                   select LoadTypeMetadata(type, metaStore);
        }

        private static IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }

        private static TypeKind GetTypeKind(Type type) // #80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        private static Tuple<Accessibility, IsSealed, IsAbstract> EmitModifiers(Type type)
        {
            Accessibility accessibility = Accessibility.Private;
            IsAbstract isAbstract = IsAbstract.NotAbstract;
            IsSealed isSealed = IsSealed.NotSealed;

            // check if not default
            if (type.IsPublic)
            {
                accessibility = Accessibility.Public;
            }
            else if (type.IsNestedPublic)
            {
                accessibility = Accessibility.Public;
            }
            else if (type.IsNestedFamily)
            {
                accessibility = Accessibility.Protected;
            }
            else if (type.IsNestedFamANDAssem)
            {
                accessibility = Accessibility.ProtectedInternal;
            }

            if (type.IsSealed)
            {
                isSealed = IsSealed.Sealed;
            }

            if (type.IsAbstract)
            {
                isAbstract = IsAbstract.Abstract;
            }

            return new Tuple<Accessibility, IsSealed, IsAbstract>(accessibility, isSealed, isAbstract);
        }
    }
}