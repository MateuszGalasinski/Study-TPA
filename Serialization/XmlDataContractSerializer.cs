using BaseCore;
using BaseCore.Model;
using Serialization.Model;
using System.IO;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;

namespace Serialization
{
    [Export(typeof(ISerializator<AssemblyBase>))]
    public class XmlDataContractSerializer : ISerializator<AssemblyBase>
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(AssemblyBase root, string filePath)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(root);
            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                _serializer.WriteObject(writer, assemblySerializationModel);
            }
        }

        public AssemblyBase Deserialize(string filePath)
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return DataTransferGraphMapper.AssemblyBase((AssemblySerializationModel)_serializer.ReadObject(reader));
            }
        }
    }
}
