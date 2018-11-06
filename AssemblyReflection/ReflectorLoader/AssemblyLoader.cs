using AssemblyReflection.Extensions;
using Core.Model;
using System;
using System.Linq;
using System.Reflection;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector
    {

        internal AssemblyMetadataStore LoadAssemblyMetadata(Assembly assembly)
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata()
            {
                Id = assembly.ManifestModule.FullyQualifiedName,
                Name = assembly.ManifestModule.Name,               
            };

            AssemblyMetadataStore metaStore = new AssemblyMetadataStore(assemblyMetadata);

            assemblyMetadata.Namespaces = (from Type type in assembly.GetTypes()
                where type.IsVisible()
                group type by type.GetNamespace() into namespaceGroup
                orderby namespaceGroup.Key
                select LoadNamespaceMetadata(namespaceGroup.Key, namespaceGroup, metaStore)).ToList<NamespaceMetadata>();

            return metaStore;
        }
    }
}