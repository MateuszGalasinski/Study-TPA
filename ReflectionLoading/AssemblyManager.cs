using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using BaseCore;
using BaseCore.Model;
using Logic.Models;

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

        public void SaveAssembly(AssemblyModel assemblyLogicReader)
        {
            Serializator.Serialize(DataTransferGraphMapper.AssemblyBase(assemblyLogicReader));
        }

        public void LoadAssemblyFromStorage()
        {
            AssemblyBase deserializedAssemblyReader = Serializator.Deserialize();

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
            string path = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(assemblyManager);

            return assemblyManager;
        }
    }
}
