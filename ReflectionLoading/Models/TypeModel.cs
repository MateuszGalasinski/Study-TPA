using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Model;
using ReflectionLoading.LogicModels;

namespace ReflectionLoading.Models
{
    public class TypeModel : BaseTypeModel
    {
        public TypeModel(LogicTypeModel type)
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

        private TypeModel(string typeName, string namespaceName, IEnumerable<BaseTypeModel> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }

        public static BaseTypeModel EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeModel(type.Name, type.GetNamespace());

            return new TypeModel(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        }

        public static List<BaseTypeModel> EmitGenericArguments(Type type)
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

        private static List<BaseParameterModel> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<BaseParameterModel> parameters = new List<BaseParameterModel>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterModel(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private BaseTypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }

        private List<BaseTypeModel> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }
            List<BaseTypeModel> typeModels = new List<BaseTypeModel>();
            foreach (var nestedType in nestedTypes)
            {
                typeModels.Add(new TypeModel(nestedType));
            }

            return typeModels;
            //return nestedTypes.Select(t => new TypeModel(t)).ToList();
        }

        private IEnumerable<BaseTypeModel> EmitImplements(IEnumerable<Type> interfaces)
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

        private static BaseTypeModel EmitExtends(Type baseType)
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
            var model = obj as BaseTypeModel;
            return model != null &&
                   Name == model.Name &&
                   NamespaceName == model.NamespaceName &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(BaseType, model.BaseType) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   Type == model.Type &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(ImplementedInterfaces, model.ImplementedInterfaces) &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(NestedTypes, model.NestedTypes) &&
                   EqualityComparer<List<BasePropertyModel>>.Default.Equals(Properties, model.Properties) &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(DeclaringType, model.DeclaringType) &&
                   EqualityComparer<List<BaseMethodModel>>.Default.Equals(Methods, model.Methods) &&
                   EqualityComparer<List<BaseMethodModel>>.Default.Equals(Constructors, model.Constructors) &&
                   EqualityComparer<List<BaseParameterModel>>.Default.Equals(Fields, model.Fields);
        }
    }
}
