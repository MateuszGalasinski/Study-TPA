using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "AssemblyModel")]
    public class SerializationAssemblyModel : BaseAssemblyModel
    {
            private SerializationAssemblyModel()
            {

            }

            public SerializationAssemblyModel(BaseAssemblyModel assemblyBase)
            {
                this.Name = assemblyBase.Name;
                NamespaceSerializationModels = new List<SerializationNamespaceModel>();
                foreach (var baseElem in base.NamespaceModels)
                {
                    NamespaceSerializationModels.Add(new SerializationNamespaceModel(baseElem));
                }

            }

            [DataMember] public List<SerializationNamespaceModel> NamespaceSerializationModels { get; set; }

            [DataMember] public new string Name { get; set; }
    }
}