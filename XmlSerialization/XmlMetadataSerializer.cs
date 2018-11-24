using System.IO;
using System.Xml.Serialization;
using ReflectionLoading.Models;

namespace XmlSerialization
{
    public class XmlMetadataSerializer
    {
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(AssemblyModel));

        public void Serialize(AssemblyModel root, string filePath)
        {
            using (TextWriter writer = new StreamWriter(filePath))
            {
                _serializer.Serialize(writer, root);
            }
        }

        public AssemblyModel Deserialize(string filePath)
        {
            using (TextReader reader = new StreamReader(filePath))
            {
                return (AssemblyModel)_serializer.Deserialize(reader);
            }
        }
    }
}
