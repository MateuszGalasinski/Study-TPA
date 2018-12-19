using BaseCore.Enums;
using System.Collections.Generic;

namespace BaseCore.Model
{
    public class TypeBase
    {

        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeBase BaseType { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public IsAbstract AbstractEnum { get; set; }

        public IsStatic StaticEnum { get; set; }

        public IsSealed SealedEnum { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeBase> ImplementedInterfaces { get; set; }

        public List<TypeBase> NestedTypes { get; set; }

        public List<PropertyBase> Properties { get; set; }

        public TypeBase DeclaringType { get; set; }

        public List<MethodBase> Methods { get; set; }

        public List<MethodBase> Constructors { get; set; }

        public List<ParameterBase> Fields { get; set; }
    }
}
