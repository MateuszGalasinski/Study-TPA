using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "NamespaceReader")]
    public class SerializationNamespaceModel : BaseNamespaceModel
    {
        
        private SerializationNamespaceModel()
            {

            }

            public SerializationNamespaceModel(BaseNamespaceModel namespaceBase)
            {
                this.Name = namespaceBase.Name;
                Types = new List<SerializationTypeModel>();
                foreach (BaseTypeModel baseElem in namespaceBase.Types)
                {
                    Types.Add(SerializationTypeModel.GetOrAdd(baseElem));
                }
            }

            [DataMember]
            public new string Name { get; set; }

            [DataMember]
            public new List<SerializationTypeModel> Types { get; set; }
        }
}