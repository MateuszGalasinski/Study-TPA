using BaseCore.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Serialization.Model
{
    [DataContract(Name = "NamespaceSerializationModel", IsReference = true)]
    public class NamespaceSerializationModel
    {
        private NamespaceSerializationModel()
        {

        }

        public NamespaceSerializationModel(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => TypeSerializationModel.GetOrAdd(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeSerializationModel> Types { get; set; }
    }
}
