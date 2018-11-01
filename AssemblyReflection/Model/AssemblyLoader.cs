using AssemblyReflection.ExtensionMethods;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyReflection.Model
{
    internal static class AssemblyLoader
    {
        internal static Dictionary<string, BaseMetadata> LoadAssemblyMetadata(Assembly assembly)
        {
            Dictionary<string, BaseMetadata> metaDictionary = new Dictionary<string, BaseMetadata>();
            if (!metaDictionary.ContainsKey(assembly.ManifestModule.Name))
            {
                AssemblyMetadata assemblyMetadata = new AssemblyMetadata()
                {
                    Name = assembly.ManifestModule.Name
                };

                metaDictionary.Add(assemblyMetadata.Name, assemblyMetadata);

                assemblyMetadata.Namespaces = (from Type type in assembly.GetTypes()
                    where type.IsVisible()
                    group type by type.GetNamespace() into namespaceGroup
                    orderby namespaceGroup.Key
                    select NamespaceLoader.LoadNamespaceMetadata(namespaceGroup.Key, namespaceGroup, metaDictionary)).ToList();
            }

            return metaDictionary;
        }
    }
}