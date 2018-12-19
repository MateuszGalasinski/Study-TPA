using Base.Model;
using Core.Components;
using System.IO;
using System.Runtime.Serialization;

namespace XmlSerialization
{
    public class XmlDataContractSerializer : ISerializator<AssemblyBase>
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(AssemblyBase));

        public void Serialize(AssemblyBase root, string filePath)
        {
            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                _serializer.WriteObject(writer, root);
            }
        }

        public AssemblyBase Deserialize(string filePath)
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return (AssemblyBase)_serializer.ReadObject(reader);
            }
        }
    }
}
