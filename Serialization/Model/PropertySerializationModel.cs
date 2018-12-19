using BaseCore.Model;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "PropertySerializationModel", IsReference = true)]
    public class PropertySerializationModel
    {
        private PropertySerializationModel()
        {

        }

        public PropertySerializationModel(PropertyBase baseProperty)
        {
            this.Name = baseProperty.Name;
            this.Type = TypeSerializationModel.GetOrAdd(baseProperty.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeSerializationModel Type { get; set; }
    }
}
