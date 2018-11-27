using Core.Components;
using ReflectionLoading.Models;
using System.IO;
using System.Runtime.Serialization;

namespace XmlSerialization
{
    public class XmlDataContractSerializer : ISerializator<AssemblyModel>
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(AssemblyModel));

        public void Serialize(AssemblyModel root, string filePath)
        {
            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                _serializer.WriteObject(writer, root);
            }
        }

        public AssemblyModel Deserialize(string filePath)
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return (AssemblyModel)_serializer.ReadObject(reader);
            }
        }
    }
}
