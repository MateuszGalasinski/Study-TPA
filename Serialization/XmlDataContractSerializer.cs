using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using BaseCore;
using BaseCore.Model;
using XmlSerialization.Model;

namespace XmlSerialization
{
    [Export(typeof(ISerializator<AssemblyBase>))]
    public class XmlDataContractSerializer : ISerializator<AssemblyBase>
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(AssemblyBase root)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(root);
            using (FileStream writer = new FileStream(ConfigurationManager.AppSettings["filePathToDataSource"], FileMode.OpenOrCreate))
            {
                _serializer.WriteObject(writer, assemblySerializationModel);
            }
        }

        public AssemblyBase Deserialize()
        {
            using (FileStream reader = new FileStream(ConfigurationManager.AppSettings["filePathToDataSource"], FileMode.Open))
            {
                return DataTransferGraphMapper.AssemblyBase((AssemblySerializationModel)_serializer.ReadObject(reader));
            }
        }
    }
}
