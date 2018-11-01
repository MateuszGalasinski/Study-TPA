using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyReflection.Model
{
    internal static class NamespaceLoader
    {
        internal static NamespaceMetadata LoadNamespaceMetadata(string name, IEnumerable<Type> types, Dictionary<string, BaseMetadata> metaDictionary)
        {
            NamespaceMetadata namespaceMetadata = new NamespaceMetadata()
            {
                Name = name
            };

            metaDictionary.Add(namespaceMetadata.Name, namespaceMetadata);

            namespaceMetadata.Types = (from type in types orderby type.Name select TypeLoader.LoadTypeMetadata(type, metaDictionary)).ToList();

            return namespaceMetadata;
        }
    }
}