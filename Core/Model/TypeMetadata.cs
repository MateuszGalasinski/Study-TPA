using Core.Constants;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class TypeMetadata
    {
        public string TypeName { get; }
        public string NamespaceName { get; }
        public TypeMetadata BaseType { get; }
        public IEnumerable<TypeMetadata> GenericArguments { get; }
        public Tuple<Accessibility, IsSealed, IsAbstract> Modifiers { get; }
        public TypeKind Kind { get; }
        public IEnumerable<Attribute> Attributes { get; }
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; }
        public IEnumerable<TypeMetadata> NestedTypes { get; }
        public IEnumerable<PropertyMetadata> Properties { get; }
        public TypeMetadata DeclaringType { get; }
        public IEnumerable<MethodMetadata> Methods { get; }
        public IEnumerable<MethodMetadata> Constructors { get; }

        public TypeMetadata(string typeName,
            string namespaceName,
            TypeMetadata baseType,
            IEnumerable<TypeMetadata> genericArguments,
            Tuple<Accessibility, IsSealed, IsAbstract> modifiers,
            TypeKind typeKind,
            IEnumerable<Attribute> attributes,
            IEnumerable<TypeMetadata> implementedInterfaces,
            IEnumerable<TypeMetadata> nestedTypes,
            IEnumerable<PropertyMetadata> properties,
            TypeMetadata declaringType,
            IEnumerable<MethodMetadata> methods,
            IEnumerable<MethodMetadata> constructors)
        {
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            NamespaceName = namespaceName ?? throw new ArgumentNullException(nameof(namespaceName));
            BaseType = baseType ?? throw new ArgumentNullException(nameof(baseType));
            GenericArguments = genericArguments ?? throw new ArgumentNullException(nameof(genericArguments));
            Modifiers = modifiers ?? throw new ArgumentNullException(nameof(modifiers));
            Kind = typeKind;
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            ImplementedInterfaces = implementedInterfaces ?? throw new ArgumentNullException(nameof(implementedInterfaces));
            NestedTypes = nestedTypes ?? throw new ArgumentNullException(nameof(nestedTypes));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
            DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
            Constructors = constructors ?? throw new ArgumentNullException(nameof(constructors));
        }
    }
}