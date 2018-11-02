using AssemblyReflection.ExtensionMethods;
using Core.Model;
using System;
using System.Linq;
using System.Reflection;

namespace AssemblyReflection.Model
{
    internal static class AssemblyLoader
    {
        internal static AssemblyMetadataStore LoadAssemblyMetadata(Assembly assembly)
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata()
            {
                Name = assembly.ManifestModule.Name
            };

            AssemblyMetadataStore metaStore = new AssemblyMetadataStore(assemblyMetadata);

            assemblyMetadata.Namespaces = (from Type type in assembly.GetTypes()
                where type.IsVisible()
                group type by type.GetNamespace() into namespaceGroup
                orderby namespaceGroup.Key
                select NamespaceLoader.LoadNamespaceMetadata(namespaceGroup.Key, namespaceGroup, metaStore)).ToList();

            return metaStore;
        }
    }
}