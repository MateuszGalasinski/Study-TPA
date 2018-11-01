using Core.Constants;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class MethodMetadata : BaseMetadata
    {
        public override string Name { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> Modifiers { get; set; }
        public TypeMetadata ReturnType { get; set; }
        public bool Extension { get; set; }
        public IEnumerable<ParameterMetadata> Parameters { get; set; }
    }
}