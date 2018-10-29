using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class AssemblyMetadata
    {
        public string Name { get; }
        public IEnumerable<NamespaceMetadata> Namespaces { get; }

        public AssemblyMetadata(string name, IEnumerable<NamespaceMetadata> namespaces)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Namespaces = namespaces ?? throw new ArgumentNullException(nameof(namespaces));
        }
    }
}