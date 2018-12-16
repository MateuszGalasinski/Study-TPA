using System.Runtime.Serialization;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "PropertyReader")]
    public class SerializationPropertyModel : BasePropertyModel
    {
        private SerializationPropertyModel()
        {

        }

        public SerializationPropertyModel(BasePropertyModel baseProperty)
        {
            this.Name = baseProperty.Name;
            this.Type = SerializationTypeModel.GetOrAdd(baseProperty.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public SerializationTypeModel Type { get; set; }
    }
}