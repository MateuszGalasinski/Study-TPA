using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyReflection.Extensions;
using Core.Constants;
using Core.Model;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector
    {
        internal TypeMetadata LoadTypeMetadataDto(Type type, AssemblyMetadataStore metaStore)
        {
            if (type == null)
            {
                throw new ArgumentNullException($"{nameof(type)} argument is null.");
            }

            if (type.IsGenericParameter)
            {
                return LoadGenericParameterTypeObject(type, metaStore);

            }
            else
            {
                if (!metaStore.TypesDictionary.ContainsKey(type.FullName))
                {
                    TypeMetadata metadataType;

                    // if type is not declared in assembly being inspected
                    if (type.Assembly.ManifestModule.FullyQualifiedName != metaStore.AssemblyMetadata.Id) // load basic info
                    {
                        metadataType = LoadSimpleTypeObject(type, metaStore);
                    }
                    else // load full type information
                    {
                        metadataType = LoadFullTypeObject(type, metaStore);
                    }

                    return metadataType;
                }
                else
                {
                    _logger.Trace("Using type already added to dictionary with key: " + type.FullName);
                    return metaStore.TypesDictionary[type.FullName];

                }
            }
        }

        private TypeMetadata LoadGenericParameterTypeObject(Type type, AssemblyMetadataStore metaStore)
        {
            TypeMetadata metadataType;
            string id = $"{type.DeclaringType.FullName}<{type.Name}>";

            _logger.Trace("Adding generic argument type with Id =" + id);

            if (!metaStore.TypesDictionary.ContainsKey(id))
            {
                metadataType = new TypeMetadata()
                {
                    Id = id,
                    Name = type.Name,
                    NamespaceName = type.Namespace,
                    Modifiers = null,
                    Kind = GetTypeKind(type),
                    Attributes = new List<Attribute>(),
                    Properties = new List<PropertyMetadata>(),
                    Constructors = new List<MethodMetadata>(),
                    ImplementedInterfaces = new List<TypeMetadata>(),
                    Methods = new List<MethodMetadata>(),
                    NestedTypes = new List<TypeMetadata>(),
                    GenericArguments = new List<TypeMetadata>()
                };
                metaStore.TypesDictionary.Add(metadataType.Id, metadataType);
                return metadataType;
            }
            else
            {
                return metaStore.TypesDictionary[id];
            }
        }

        private TypeMetadata LoadSimpleTypeObject(Type type, AssemblyMetadataStore metaStore)
        {
            TypeMetadata metadataType;
            metadataType = new TypeMetadata // add only basic information
            {
                Id = type.FullName,
                Name = type.Name,
                NamespaceName = type.Namespace,
                Modifiers = EmitModifiers(type),
                Kind = GetTypeKind(type),
                Attributes = type.GetCustomAttributes(false).Cast<Attribute>(),
                Properties = new List<PropertyMetadata>(),
                Constructors = new List<MethodMetadata>(),
                ImplementedInterfaces = new List<TypeMetadata>(),
                Methods = new List<MethodMetadata>(),
                NestedTypes = new List<TypeMetadata>()
            };

            metaStore.TypesDictionary.Add(type.FullName, metadataType);

            if (type.IsGenericTypeDefinition)
            {
                metadataType.GenericArguments = EmitGenericArguments(type.GetGenericArguments(), metaStore);
                metadataType.Name =
                    $"{type.Name}<{metadataType.GenericArguments.Select(a => a.Name).Aggregate((p, n) => $"{p}, {n}")}>";
            }
            else
            {
                metadataType.GenericArguments = new List<TypeMetadata>();
            }

            _logger.Trace("Adding type not declared in assembly being inspected: Id =" + metadataType.Id + " ; Name = " + metadataType.Name);
            return metadataType;
        }

        private TypeMetadata LoadFullTypeObject(Type type, AssemblyMetadataStore metaStore)
        {
            TypeMetadata metadataType = new TypeMetadata()
            {
                Id = type.FullName,
                Name = type.Name,
                NamespaceName = type.Namespace,
                Modifiers = EmitModifiers(type),
                Kind = GetTypeKind(type),
                Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
            };
            _logger.Trace("Adding type: Id =" + metadataType.Id + " ; Name = " + metadataType.Name);
            metaStore.TypesDictionary.Add(type.FullName, metadataType);

            metadataType.DeclaringType = EmitDeclaringType(type.DeclaringType, metaStore);
            metadataType.ImplementedInterfaces = EmitImplements(type.GetInterfaces(), metaStore);
            metadataType.BaseType = EmitExtends(type.BaseType, metaStore);
            metadataType.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), metaStore);

            if (type.IsGenericTypeDefinition)
            {
                metadataType.GenericArguments = EmitGenericArguments(type.GetGenericArguments(), metaStore);
                metadataType.Name =
                    $"{type.Name}<{metadataType.GenericArguments.Select(a => a.Name).Aggregate((p, n) => $"{p}, {n}")}>";
            }
            else
            {
                metadataType.GenericArguments = new List<TypeMetadata>();
            }

            metadataType.Constructors = EmitMethods(type.GetConstructors(), metaStore);
            metadataType.Methods = EmitMethods(type.GetMethods(BindingFlags.DeclaredOnly), metaStore);

            metadataType.Properties = EmitProperties(type.GetProperties(), metaStore);
            return metadataType;
        }

        internal IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments, AssemblyMetadataStore metaStore)
        {
            return (from Type argument in arguments select LoadTypeMetadataDto(argument, metaStore)).ToList();
        }

        private TypeMetadata EmitExtends(Type baseType, AssemblyMetadataStore metaStore)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
            {
                return null;
            }

            return LoadTypeMetadataDto(baseType, metaStore);
        }

        private TypeMetadata EmitDeclaringType(Type declaringType, AssemblyMetadataStore metaStore)
        {
            if (declaringType == null)
            {
                return null;
            }

            return LoadTypeMetadataDto(declaringType, metaStore);
        }

        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes, AssemblyMetadataStore metaStore)
        {
            return (from type in nestedTypes
                    where type.IsVisible()
                    select LoadTypeMetadataDto(type, metaStore)).ToList();
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces, AssemblyMetadataStore metaStore)
        {
            return (from currentInterface in interfaces
                    select LoadTypeMetadataDto(currentInterface, metaStore)).ToList();
        }

        private TypeKind GetTypeKind(Type type) // #80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        private Tuple<Accessibility, IsSealed, IsAbstract> EmitModifiers(Type type)
        {
            Accessibility accessLevel = Accessibility.Private;
            // check if not default
            if (type.IsPublic)
            {
                accessLevel = Accessibility.Public;
            }
            else if (type.IsNestedPublic)
            {
                accessLevel = Accessibility.Public;
            }
            else if (type.IsNestedFamily)
            {
                accessLevel = Accessibility.Protected;
            }
            else if (type.IsNestedFamANDAssem)
            {
                accessLevel = Accessibility.ProtectedInternal;
            }

            IsSealed sealedEnum = IsSealed.NotSealed;
            if (type.IsSealed)
            {
                sealedEnum = IsSealed.Sealed;
            }

            IsAbstract abstractEnum = IsAbstract.NotAbstract;
            if (type.IsAbstract)
            {
                abstractEnum = IsAbstract.Abstract;
            }

            return new Tuple<Accessibility, IsSealed, IsAbstract>(accessLevel, sealedEnum, abstractEnum);
        }
    }
}