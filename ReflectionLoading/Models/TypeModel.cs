using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Constants;
using Reflection.Model;

namespace ReflectionLoading.Models
{
    public class TypeModel
    {
        
        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();
        
        public string Name { get; set; }
        
        public string NamespaceName { get; set; }
        
        public TypeModel BaseType { get; set; }
        
        public List<TypeModel> GenericArguments { get; set; }
        
        public Tuple<Accessibility, IsSealed, IsAbstract, IsStatic> Modifiers { get; set; }
        
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
            Modifiers = EmitModifiers(type);

            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodModel.EmitConstructors(type);
            Methods = MethodModel.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = PropertyModel.EmitProperties(type);
            Fields = EmitFields(type);
        }

        private TypeModel(string typeName, string namespaceName)
        {
            Name = typeName;
            this.NamespaceName = namespaceName;
        }

        private TypeModel(string typeName, string namespaceName, IEnumerable<TypeModel> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }


        public static TypeModel EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeModel(type.Name, type.GetNamespace());

            return new TypeModel(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        }
        public static List<TypeModel> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(EmitReference).ToList();
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
                parameters.Add(new ParameterModel(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
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
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }

        static Tuple<Accessibility, IsSealed, IsAbstract, IsStatic> EmitModifiers(Type type)
        {
            Accessibility _access = type.IsPublic || type.IsNestedPublic ? Accessibility.Public :
                type.IsNestedFamily ? Accessibility.Protected :
                type.IsNestedFamANDAssem ? Accessibility.ProtectedInternal :
                Accessibility.Private;
            IsStatic _static = type.IsSealed && type.IsAbstract ? IsStatic.Static : IsStatic.NotStatic;
            IsSealed _sealed = IsSealed.NotSealed;
            IsAbstract _abstract = IsAbstract.NotAbstract;
            if (_static == IsStatic.NotStatic)
            {
                _sealed = type.IsSealed ? IsSealed.Sealed : IsSealed.NotSealed;
                _abstract = type.IsAbstract ? IsAbstract.Abstract : IsAbstract.NotAbstract;
            }



            return new Tuple<Accessibility, IsSealed, IsAbstract, IsStatic>(_access, _sealed, _abstract,_static);
        }

        private static TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
    }
}
