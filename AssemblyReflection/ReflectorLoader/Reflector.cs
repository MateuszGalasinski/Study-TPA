using Core.Components;
using Core.Model;
using System.Reflection;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector : IStoreProvider
    {
        private ILogger _logger;

        public Reflector(ILogger logger)
        {
            _logger = logger;
        }

        public AssemblyMetadataStore GetAssemblyMetadataStore(string assemblyFile)
        {
            
            if (string.IsNullOrEmpty(assemblyFile))
            {
                throw new System.ArgumentNullException($"Could not find assembly file such with path: {assemblyFile}");
            }

            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            _logger.Trace("Opening assembly: " + assembly.FullName);

            return LoadAssemblyMetadata(assembly);
        }
    }
}
