using System.Collections.Generic;

namespace Core.Model
{
    public class MetadataStore
    {
        public AssemblyMetadata AssemblyMetadata { get; }
        public HashSet<string> Identifiers { get; }
        public Dictionary<string, NamespaceMetadata> NamespacesDictionary { get; }
        public Dictionary<string, TypeMetadata> TypesDictionary { get; }
        public Dictionary<string, PropertyMetadata> PropertiesDictionary { get; }
        public Dictionary<string, MethodMetadata> MethodsDictionary { get; }
        public Dictionary<string, ParameterMetadata> ParametersDictionary { get; }
    }
}
