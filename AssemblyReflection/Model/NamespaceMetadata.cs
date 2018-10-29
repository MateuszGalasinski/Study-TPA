using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyReflection.Model
{
    internal class NamespaceMetadata
    {
        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            _namespaceName = name;
            _types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        private string _namespaceName;
        private IEnumerable<TypeMetadata> _types;
    }
}