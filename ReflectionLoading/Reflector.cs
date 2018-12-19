using Logic.Models;
using System.Reflection;

namespace ReflectionLoading
{
    public class Reflector
    {
        public AssemblyModel LoadAssembly(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            TypeModel.TypeDictionary.Clear();
            return new AssemblyModel(assembly);
        }
    }
}
