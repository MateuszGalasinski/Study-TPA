using Base.Enums;
using Base.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "MethodSerializationModel", IsReference = true)]
    public class MethodSerializationModel
    {
        private MethodSerializationModel()
        {
        }

        public MethodSerializationModel(MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeSerializationModel.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeSerializationModel.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterSerializationModel(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeSerializationModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public StaticEnum StaticEnum { get; set; }

        [DataMember]
        public VirtualEnum VirtualEnum { get; set; }

        [DataMember]
        public TypeSerializationModel ReturnType { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public List<ParameterSerializationModel> Parameters { get; set; }

    }
}
