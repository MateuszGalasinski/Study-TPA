using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class NamespaceMetadata
    {
        public string NamespaceName { get; }
        public IEnumerable<TypeMetadata> Types { get; }

        public NamespaceMetadata(string namespaceName, IEnumerable<TypeMetadata> types)
        {
            NamespaceName = namespaceName ?? throw new ArgumentNullException(nameof(namespaceName));
            Types = types ?? throw new ArgumentNullException(nameof(types));
        }
    }
}