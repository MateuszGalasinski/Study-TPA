using Core.Constants;
using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "TypeModel")]
    public class TypeModel
    {
        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();
        
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }
        [DataMember]
        public TypeModel BaseType { get; set; }
        [DataMember]
        public Accessibility Accessibility { get; set; }
        [DataMember]
        public IsSealed IsSealed { get; set; }
        [DataMember]
        public IsAbstract IsAbstract { get; set; }
        [DataMember]
        public IsStatic IsStatic { get; set; }
        [DataMember]
        public TypeKind Type { get; set; }

        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }
        [DataMember]
        public List<TypeModel> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeModel> NestedTypes { get; set; }
        [DataMember]
        public List<PropertyModel> Properties { get; set; }
        [DataMember]
        public TypeModel DeclaringType { get; set; }
        [DataMember]
        public List<MethodModel> Methods { get; set; }
        [DataMember]
        public List<MethodModel> Constructors { get; set; }
        [DataMember]
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
            LoadModifiers(type);

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

        private void LoadModifiers(Type type)
        {
            Tuple<Accessibility, IsSealed, IsAbstract, IsStatic> modifiers = EmitModifiers(type);
            Accessibility = modifiers.Item1;
            IsSealed = modifiers.Item2;
            IsAbstract = modifiers.Item3;
            IsStatic = modifiers.Item4;
        }

        public override bool Equals(object obj)
        {
            var model = obj as TypeModel;
            return model != null &&
                   Name == model.Name &&
                   NamespaceName == model.NamespaceName &&
                   EqualityComparer<TypeModel>.Default.Equals(BaseType, model.BaseType) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   Type == model.Type &&
                   EqualityComparer<List<TypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   EqualityComparer<List<TypeModel>>.Default.Equals(ImplementedInterfaces, model.ImplementedInterfaces) &&
                   EqualityComparer<List<TypeModel>>.Default.Equals(NestedTypes, model.NestedTypes) &&
                   EqualityComparer<List<PropertyModel>>.Default.Equals(Properties, model.Properties) &&
                   EqualityComparer<TypeModel>.Default.Equals(DeclaringType, model.DeclaringType) &&
                   EqualityComparer<List<MethodModel>>.Default.Equals(Methods, model.Methods) &&
                   EqualityComparer<List<MethodModel>>.Default.Equals(Constructors, model.Constructors) &&
                   EqualityComparer<List<ParameterModel>>.Default.Equals(Fields, model.Fields);
        }
    }
}
