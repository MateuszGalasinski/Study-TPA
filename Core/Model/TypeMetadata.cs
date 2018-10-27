using Core.Constants;
using Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    internal class TypeMetadata
    {
        internal enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }

        private readonly string _typeName;
        private string _namespaceName;
        private TypeMetadata _baseType;
        private IEnumerable<TypeMetadata> _genericArguments;
        private Tuple<Accessibility, IsSealed, IsAbstract> _modifiers;
        private TypeKind _typeKind;
        private IEnumerable<Attribute> _attributes;
        private IEnumerable<TypeMetadata> _implementedInterfaces;
        private IEnumerable<TypeMetadata> _nestedTypes;
        private IEnumerable<PropertyMetadata> _properties;
        private TypeMetadata _declaringType;
        private IEnumerable<MethodMetadata> _methods;
        private IEnumerable<MethodMetadata> _constructors;

        internal TypeMetadata(Type type)
        {
            _typeName = type.Name;
            _declaringType = EmitDeclaringType(type.DeclaringType);
            _constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            _methods = MethodMetadata.EmitMethods(type.GetMethods());
            _nestedTypes = EmitNestedTypes(type.GetNestedTypes());
            _implementedInterfaces = EmitImplements(type.GetInterfaces());
            _genericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            _modifiers = EmitModifiers(type);
            _baseType = EmitExtends(type.BaseType);
            _properties = PropertyMetadata.EmitProperties(type.GetProperties());
            _typeKind = GetTypeKind(type);
            _attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }

        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return new TypeMetadata(type.Name, type.GetNamespace());
            }
            else
            {
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
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

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
            {
                return null;
            }

            return EmitReference(baseType);
        }

        private TypeMetadata(string typeName, string namespaceName)
        {
            this._typeName = typeName;
            _namespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments)
            : this(typeName, namespaceName)
        {
            _genericArguments = genericArguments;
        }

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
            {
                return null;
            }

            return EmitReference(declaringType);
        }

        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from type in nestedTypes
                   where type.IsVisible()
                   select new TypeMetadata(type);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }

    }
}