using System;
using System.Collections.Generic;
using Core.Constants;

namespace Core.Model
{
    public abstract class BaseTypeModel
    {
        public static Dictionary<string, BaseTypeModel> TypeDictionary = new Dictionary<string, BaseTypeModel>();

        public string Name { get; set; }
        public string NamespaceName { get; set; }
        public BaseTypeModel BaseType { get; set; }
        public Accessibility Accessibility { get; set; }
        public IsSealed IsSealed { get; set; }
        public IsAbstract IsAbstract { get; set; }
        public IsStatic IsStatic { get; set; }
        public TypeKind Type { get; set; }

        public List<BaseTypeModel> GenericArguments { get; set; }
        public List<BaseTypeModel> ImplementedInterfaces { get; set; }
        public List<BaseTypeModel> NestedTypes { get; set; }
        public List<BasePropertyModel> Properties { get; set; }
        public BaseTypeModel DeclaringType { get; set; }
        public List<BaseMethodModel> Methods { get; set; }
        public List<BaseMethodModel> Constructors { get; set; }
        public List<BaseParameterModel> Fields { get; set; }

       
    }
}