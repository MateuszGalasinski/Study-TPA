using BaseCore.Model;
using System.Runtime.Serialization;

namespace Serialization.Model
{
    [DataContract(Name = "ParameterSerializationModel", IsReference = true)]
    public class ParameterSerializationModel
    {
        private ParameterSerializationModel()
        {

        }

        public ParameterSerializationModel(ParameterBase baseParameter)
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
