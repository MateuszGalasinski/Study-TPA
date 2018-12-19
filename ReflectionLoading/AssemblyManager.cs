using BaseCore;
using BaseCore.Model;
using Logic.Models;
using System;

namespace ReflectionLoading
{
    public class AssemblyManager
    {
        public AssemblyModel AssemblyModel { get; private set; }

        private ISerializator<AssemblyBase> Serializator { get; }
        private Reflector Reflector { get; } = new Reflector();

        public AssemblyManager(ISerializator<AssemblyBase> serializator)
        {
            Serializator = serializator ?? throw new ArgumentNullException(nameof(serializator));
        }


        public AssemblyManager(AssemblyModel assemblyReader)
        {
            AssemblyModel = assemblyReader;
            TypeModel.TypeDictionary.Clear();
        }

        public void SaveAssembly(AssemblyModel assemblyLogicReader, string connectionString)
        {
            Serializator.Serialize(DataTransferGraphMapper.AssemblyBase(assemblyLogicReader), connectionString);
        }

        public void LoadAssemblyFromStorage(string connectionString)
        {
            AssemblyBase deserializedAssemblyReader = Serializator.Deserialize(connectionString);

            AssemblyModel = new AssemblyModel(deserializedAssemblyReader);
        }

        public void LoadAssemblyFromLibrary(string assemblyPath)
        {
            AssemblyModel = Reflector.LoadAssembly(assemblyPath);
        }
    }
}
