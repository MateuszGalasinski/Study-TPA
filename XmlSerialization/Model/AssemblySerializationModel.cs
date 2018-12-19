using Base.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "AssemblySerializationModel", IsReference = true)]
    public class AssemblySerializationModel
    {
        private AssemblySerializationModel()
        {

        }

        public AssemblySerializationModel(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(ns => new NamespaceSerializationModel(ns)).ToList();

        }

        [DataMember]
        public List<NamespaceSerializationModel> Namespaces { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
