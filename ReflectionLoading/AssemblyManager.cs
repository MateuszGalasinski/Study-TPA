using BaseCore;
using BaseCore.Model;
using Logic.Models;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ReflectionLoading
{
    public class AssemblyManager
    {
        public AssemblyModel AssemblyModel { get; set; }
        [Import(typeof(ISerializator<AssemblyBase>))]
        public ISerializator<AssemblyBase> Serializer { get; set; }

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
            Serializer.Serialize(DataTransferGraphMapper.AssemblyBase(assemblyLogicReader));
        }

        public void LoadAssemblyFromStorage()
        {
            AssemblyBase deserializedAssemblyReader = Serializer.Deserialize();

            AssemblyModel = new AssemblyModel(deserializedAssemblyReader);
        }

        public void LoadAssemblyFromLibrary(string assemblyPath)
        {
            AssemblyModel = Reflector.LoadAssembly(assemblyPath);
        }

        public static AssemblyManager GetComposed(CompositionContainer container)
        {
            AssemblyManager assemblyManager = new AssemblyManager();

            container.ComposeParts(assemblyManager);

            return assemblyManager;
        }
    }
}
