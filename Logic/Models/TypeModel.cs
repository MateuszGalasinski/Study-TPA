using Base.Model;
using Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logic.Models
{
    public class TypeModel
    {
        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();

        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeModel BaseType { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsSealed IsSealed { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeModel> ImplementedInterfaces { get; set; }

        public List<TypeModel> NestedTypes { get; set; }

        public List<PropertyModel> Properties { get; set; }

        public TypeModel DeclaringType { get; set; }

        public List<MethodModel> Methods { get; set; }

        public List<MethodModel> Constructors { get; set; }

        public List<ParameterModel> Fields { get; set; }

        public TypeModel(Type type)
        {
            Name = type.Name;
            if (!TypeDictionary.ContainsKey(Name))
            {
                TypeDictionary.Add(Name, this);
            }

            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            EmitModifiers(type);
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodModel.EmitConstructors(type);
            Methods = MethodModel.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? new List<TypeModel>() : EmitGenericArguments(type);
            Properties = PropertyModel.EmitProperties(type);
            Fields = EmitFields(type);
        }

        private TypeModel(TypeBase baseType)
        {
            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type.ToLogicEnum();

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.IsAbstract = baseType.AbstractEnum.ToLogicEnum();
            this.Accessibility = baseType.AccessLevel.ToLogicEnum();
            this.IsSealed = baseType.SealedEnum.ToLogicEnum();
            this.IsStatic = baseType.StaticEnum.ToLogicEnum();

            Constructors = baseType.Constructors?.Select(c => new MethodModel(c)).ToList();

            Fields = baseType.Fields?.Select(t => new ParameterModel(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodModel(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertyModel(t)).ToList();
        }

        public static TypeModel GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (TypeDictionary.ContainsKey(baseType.Name))
                {
                    return TypeDictionary[baseType.Name];
                }
                else
                {
                    return new TypeModel(baseType);
                }
            }
            else
                return null;
        }

        public static TypeModel GetOrAdd(Type type)
        {
            if (TypeDictionary.ContainsKey(type.Name))
            {
                return TypeDictionary[type.Name];
            }
            else
            {
                return new TypeModel(type);
            }
        }

        public static List<TypeModel> EmitGenericArguments(Type type)
        {
            if (!type.ContainsGenericParameters)
            {
                return  new List<TypeModel>();
            }
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(GetOrAdd).ToList();
        }

        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeModel(type);
            }
        }

        private static List<ParameterModel> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterModel> parameters = new List<ParameterModel>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterModel(field.Name, GetOrAdd(field.FieldType)));
            }
            return parameters;
        }

        private TypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return GetOrAdd(declaringType);
        }
        private List<TypeModel> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new TypeModel(t)).ToList();
        }
        private IEnumerable<TypeModel> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select GetOrAdd(currentInterface);
        }
        private static TypeKind GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }

        private void EmitModifiers(Type type)
        {
            Accessibility = type.IsPublic || type.IsNestedPublic ? Accessibility.IsPublic :
                type.IsNestedFamily ? Accessibility.IsProtected :
                type.IsNestedFamANDAssem ? Accessibility.Internal :
                Accessibility.IsPrivate;
            IsStatic = type.IsSealed && type.IsAbstract ? IsStatic.Static : IsStatic.NotStatic;
            IsSealed = IsSealed.NotSealed;
            IsAbstract = IsAbstract.NotAbstract;
            if (IsStatic == IsStatic.NotStatic)
            {
                IsSealed = type.IsSealed ? IsSealed.Sealed : IsSealed.NotSealed;
                IsAbstract = type.IsAbstract ? IsAbstract.Abstract : IsAbstract.NotAbstract;
            }
        }

        private static TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            if (baseType.Name == "Exception")
            {
                Console.WriteLine("EEEE");
            }
            return GetOrAdd(baseType);
        }
    }
}