using System.Runtime.Serialization;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "ParameterReader")]
    public class SerializationParameterModel : BaseParameterModel
    {
        private SerializationParameterModel()
        {

        }

        public SerializationParameterModel(BaseParameterModel baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = SerializationTypeModel.GetOrAdd(baseParameter.Type);
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new SerializationTypeModel Type { get; set; }

    }
}