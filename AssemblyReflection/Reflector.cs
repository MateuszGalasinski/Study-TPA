using Core.Components;
using System.Reflection;

namespace AssemblyReflection
{
    public class Reflector : IDataSource
    {
        public Core.Model.AssemblyMetadata GetAssemblyMetadata(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            
            //TODO: implement real mapping
            return new Core.Model.AssemblyMetadata(null, null);
        }
    }
}
