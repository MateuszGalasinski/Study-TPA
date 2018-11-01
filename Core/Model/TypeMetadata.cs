using Core.Constants;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class TypeMetadata : BaseMetadata
    {
        public override string Name { get; set; }
        public string NamespaceName { get; set; }
        public TypeMetadata BaseType { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<Accessibility, IsSealed, IsAbstract> Modifiers { get; set; }
        public TypeKind Kind { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; set; }
        public IEnumerable<TypeMetadata> NestedTypes { get; set; }
        public IEnumerable<PropertyMetadata> Properties { get; set; }
        public TypeMetadata DeclaringType { get; set; }
        public IEnumerable<MethodMetadata> Methods { get; set; }
        public IEnumerable<MethodMetadata> Constructors { get; set; }
    }
}