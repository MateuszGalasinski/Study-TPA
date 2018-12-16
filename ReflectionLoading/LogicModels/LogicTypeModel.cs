using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Constants;

namespace ReflectionLoading.LogicModels
{
    public class LogicTypeModel
    {
        public static Dictionary<string, LogicTypeModel> TypeDictionary = new Dictionary<string, LogicTypeModel>();
        
        public string Name { get; set; }
        
        public string NamespaceName { get; set; }
        
        public LogicTypeModel BaseType { get; set; }
        
        public Accessibility Accessibility { get; set; }
        
        public IsSealed IsSealed { get; set; }
        
        public IsAbstract IsAbstract { get; set; }
        
        public IsStatic IsStatic { get; set; }
        
        public TypeKind Type { get; set; }

        
        public List<LogicTypeModel> GenericArguments { get; set; }
        
        public List<LogicTypeModel> ImplementedInterfaces { get; set; }
        
        public List<LogicTypeModel> NestedTypes { get; set; }
        
        public List<LogicPropertyModel> Properties { get; set; }
        
        public LogicTypeModel DeclaringType { get; set; }
        
        public List<LogicMethodModel> Methods { get; set; }
        
        public List<LogicModels.LogicMethodModel> Constructors { get; set; }
        
        public List<LogicModels.LogicParameterModel> Fields { get; set; }

        public LogicTypeModel(Type type)
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
            Constructors = LogicModels.LogicMethodModel.EmitConstructors(type);
            Methods = LogicModels.LogicMethodModel.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = LogicModels.LogicPropertyModel.EmitProperties(type);
            Fields = EmitFields(type);
        }

        public LogicTypeModel(LogicTypeModel type)
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
            Constructors = LogicMethodModel.EmitConstructors(type);
            Methods = LogicMethodModel.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = LogicPropertyModel.EmitProperties(type);
            Fields = EmitFields(type);
        }


        private LogicTypeModel(string typeName, string namespaceName)
        {
            Name = typeName;
            this.NamespaceName = namespaceName;
        }

        private LogicTypeModel(string typeName, string namespaceName, IEnumerable<LogicTypeModel> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }

        #region 

        public static LogicTypeModel EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new LogicTypeModel(type.Name, LogicModels.ExtensionMethods.GetNamespace(type));

            return new LogicTypeModel(type.Name, LogicModels.ExtensionMethods.GetNamespace(type), EmitGenericArguments(type));
        }

        public static List<LogicTypeModel> EmitGenericArguments(Type type)
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
                new LogicTypeModel(type);
            }
        }

        private static List<LogicModels.LogicParameterModel> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<LogicModels.LogicParameterModel> parameters = new List<LogicModels.LogicParameterModel>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new LogicModels.LogicParameterModel(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private LogicTypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }

        private List<LogicTypeModel> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new LogicTypeModel(t)).ToList();
        }

        private IEnumerable<LogicTypeModel> EmitImplements(IEnumerable<Type> interfaces)
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

        private static LogicTypeModel EmitExtends(Type baseType)
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

#endregion

        public override bool Equals(object obj)
        {
            var model = obj as LogicTypeModel;
            return model != null &&
                   Name == model.Name &&
                   NamespaceName == model.NamespaceName &&
                   EqualityComparer<LogicTypeModel>.Default.Equals(BaseType, model.BaseType) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   Type == model.Type &&
                   EqualityComparer<List<LogicTypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   EqualityComparer<List<LogicTypeModel>>.Default.Equals(ImplementedInterfaces, model.ImplementedInterfaces) &&
                   EqualityComparer<List<LogicTypeModel>>.Default.Equals(NestedTypes, model.NestedTypes) &&
                   EqualityComparer<List<LogicModels.LogicPropertyModel>>.Default.Equals(Properties, model.Properties) &&
                   EqualityComparer<LogicTypeModel>.Default.Equals(DeclaringType, model.DeclaringType) &&
                   EqualityComparer<List<LogicModels.LogicMethodModel>>.Default.Equals(Methods, model.Methods) &&
                   EqualityComparer<List<LogicModels.LogicMethodModel>>.Default.Equals(Constructors, model.Constructors) &&
                   EqualityComparer<List<LogicModels.LogicParameterModel>>.Default.Equals(Fields, model.Fields);
        }
    }
}
