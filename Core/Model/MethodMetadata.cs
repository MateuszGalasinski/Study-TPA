using Core.Constants;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class MethodMetadata
    {
        public string Name { get; }
        public IEnumerable<TypeMetadata> GenericArguments { get; }
        public Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> Modifiers { get; }
        public TypeMetadata ReturnType { get; }
        public bool Extension { get; }
        public IEnumerable<ParameterMetadata> Parameters { get; }

        public MethodMetadata(
            string name,
            IEnumerable<TypeMetadata> genericArguments,
            Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> modifiers,
            TypeMetadata returnType,
            bool extension,
            IEnumerable<ParameterMetadata> parameters)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            GenericArguments = genericArguments ?? throw new ArgumentNullException(nameof(genericArguments));
            Modifiers = modifiers ?? throw new ArgumentNullException(nameof(modifiers));
            ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            Extension = extension;
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }
    }
}