using System.Collections.Generic;
using BaseCore.Enums;

namespace BaseCore.Model
{
    public class TypeBase
    {
        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeBase BaseType { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsSealed IsSealed { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeBase> ImplementedInterfaces { get; set; }

        public List<TypeBase> NestedTypes { get; set; }

        public List<PropertyBase> Properties { get; set; }

        public TypeBase DeclaringType { get; set; }

        public List<MethodBase> Methods { get; set; }

        public List<MethodBase> Constructors { get; set; }

        public List<FieldBase> Fields { get; set; }
    }
}
