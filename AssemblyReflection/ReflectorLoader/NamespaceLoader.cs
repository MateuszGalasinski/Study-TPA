using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector
    {
        internal NamespaceMetadata LoadNamespaceMetadata(string name, IEnumerable<Type> types, AssemblyMetadataStore metaStore)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} is null/empty/whitespace");
            }

            NamespaceMetadata namespaceMetadata = new NamespaceMetadata()
            {
                Id = name,
                Name = name
            };

            _logger.Trace("Adding Namespace to dictionary: " + namespaceMetadata.Name);
            metaStore.NamespacesDictionary.Add(namespaceMetadata.Name, namespaceMetadata);

            namespaceMetadata.Types = (from type in types orderby type.Name select LoadTypeMetadataDto(type, metaStore)).ToList();

            return namespaceMetadata;
        }
    }
}