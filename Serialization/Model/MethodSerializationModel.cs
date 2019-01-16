using BaseCore.Enums;
using BaseCore.Model;
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
            this.IsAbstract = baseMethod.IsAbstract;
            this.Accessibility = baseMethod.Accessibility;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeSerializationModel.GetOrAdd(baseMethod.ReturnType);
            this.IsStatic = baseMethod.IsStatic;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeSerializationModel.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterSerializationModel(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeSerializationModel> GenericArguments { get; set; }

        [DataMember]
        public Accessibility Accessibility { get; set; }

        [DataMember]
        public IsAbstract IsAbstract { get; set; }

        [DataMember]
        public IsStatic IsStatic { get; set; }

        [DataMember]
        public IsVirtual VirtualEnum { get; set; }

        [DataMember]
        public TypeSerializationModel ReturnType { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public List<ParameterSerializationModel> Parameters { get; set; }

    }
}
