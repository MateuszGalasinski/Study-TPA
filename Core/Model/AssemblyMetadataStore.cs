using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class AssemblyMetadataStore
    {
        public AssemblyMetadata AssemblyMetadata { get; }
        public Dictionary<string, NamespaceMetadata> NamespacesDictionary { get; }
        public Dictionary<string, TypeMetadata> TypesDictionary { get; }
        public Dictionary<string, PropertyMetadata> PropertiesDictionary { get; }
        public Dictionary<string, MethodMetadata> MethodsDictionary { get; }
        public Dictionary<string, ParameterMetadata> ParametersDictionary { get; }

        public AssemblyMetadataStore(AssemblyMetadata assemblyMetadata)
        {
            AssemblyMetadata = assemblyMetadata ?? throw new ArgumentNullException(nameof(assemblyMetadata));
            NamespacesDictionary = new Dictionary<string, NamespaceMetadata>();
            TypesDictionary = new Dictionary<string, TypeMetadata>();
            PropertiesDictionary = new Dictionary<string, PropertyMetadata>();
            MethodsDictionary = new Dictionary<string, MethodMetadata>();
            ParametersDictionary = new Dictionary<string, ParameterMetadata>();
        }
    }
}
