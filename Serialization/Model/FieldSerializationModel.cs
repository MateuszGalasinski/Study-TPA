using System.Runtime.Serialization;
using BaseCore.Model;
using XmlSerialization.Model;

namespace Serialization.Model
{
    [DataContract(Name = "ParameterSerializationModel", IsReference = true)]

    public class FieldSerializationModel
    {
        private FieldSerializationModel()
        {

        }

        public FieldSerializationModel(FieldBase baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = TypeSerializationModel.GetOrAdd(baseParameter.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeSerializationModel Type { get; set; }

    }
}