using BaseCore;
using BaseCore.Model;
using Logic.Models;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ReflectionLoading
{
    public class AssemblyManager
    {
        public AssemblyModel AssemblyModel { get; private set; }
        [Import(typeof(ISerializator<AssemblyBase>))]
        public ISerializator<AssemblyBase> Serializator { get; set; }

        private Reflector Reflector { get; } = new Reflector();

        public AssemblyManager()
        {
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

        public static AssemblyManager GetComposed()
        {
            AssemblyManager assemblyManager = new AssemblyManager();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../Serialization/bin/Debug"));
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(assemblyManager);

            return assemblyManager;
        }
    }
}
