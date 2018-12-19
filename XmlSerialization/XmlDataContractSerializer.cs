using BaseCore;
using BaseCore.Model;
using System.IO;
using System.Runtime.Serialization;
using XmlSerialization.Model;

namespace XmlSerialization
{
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
