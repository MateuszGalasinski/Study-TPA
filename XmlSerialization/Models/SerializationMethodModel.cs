using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Core.Constants;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "MethodReader")]
    public class SerializationMethodModel : BaseMethodModel
    {
        
            private SerializationMethodModel()
            {
            }

            public SerializationMethodModel(BaseMethodModel baseMethod)
            {
                this.Name = baseMethod.Name;
                this.AbstractEnum = baseMethod.IsAbstract;
                this.AccessLevel = baseMethod.Accessibility;
                this.Extension = baseMethod.Extension;
                this.ReturnType = SerializationTypeModel.GetOrAdd(baseMethod.ReturnType);
                this.StaticEnum = baseMethod.IsStatic;
                this.VirtualEnum = baseMethod.IsVirtual;
                foreach (var baseGenericArgument in baseMethod.GenericArguments)
                {
                    this.GenericArguments.Add(SerializationTypeModel.GetOrAdd(baseGenericArgument));
                }

                foreach (var baseParameter in baseMethod.Parameters)
                {
                    this.Parameters.Add(new SerializationParameterModel(baseParameter));
                }
            }

            [DataMember]
            public new string Name { get; set; }

            [DataMember]
            public new List<SerializationTypeModel> GenericArguments { get; set; }

            [DataMember]
            public new Accessibility AccessLevel { get; set; }

            [DataMember]
            public new IsAbstract AbstractEnum { get; set; }

            [DataMember]
            public new IsStatic StaticEnum { get; set; }

            [DataMember]
            public new IsVirtual VirtualEnum { get; set; }

            [DataMember]
            public new SerializationTypeModel ReturnType { get; set; }

            [DataMember]
            public new bool Extension { get; set; }

            [DataMember]
            public new List<SerializationParameterModel> Parameters { get; set; }

        }
    }