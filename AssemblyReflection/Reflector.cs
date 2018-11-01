using AssemblyReflection.Model;
using Core.Components;
using Core.Model;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyReflection
{
    public class Reflector : IDataSourceProvider
    {
        public Dictionary<string, BaseMetadata> GetAssemblyMetadata(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
            {
                throw new System.ArgumentNullException($"Could not find assembly file such with path: {assemblyFile}");
            }

            Assembly assembly = Assembly.LoadFrom(assemblyFile);

            return AssemblyLoader.LoadAssemblyMetadata(assembly);
        }
    }
}
