using Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Model
{
    public class AssemblyMetadata
    {
        internal AssemblyMetadata(Assembly assembly)
        {
            _name = assembly.ManifestModule.Name;
            _namespaces = from Type type in assembly.GetTypes()
                          where type.IsVisible()
                          group type by type.GetNamespace() into namespaceGroup
                          orderby namespaceGroup.Key
                          select new NamespaceMetadata(namespaceGroup.Key, namespaceGroup);
        }

        private string _name;
        private IEnumerable<NamespaceMetadata> _namespaces;
    }
}