using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Constants;
using Core.Model;

namespace XmlSerialization.Models
{
    [DataContract(Name = "TypeModel")]
    public class SerializationTypeModel : BaseTypeModel
    {
        private SerializationTypeModel(BaseTypeModel baseType)
        {
            TypeDictionary.Add(Name, this);

            this.Name = baseType.Name;
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.IsAbstract;
            this.AccessLevel = baseType.Accessibility;
            this.SealedEnum = baseType.IsSealed;
            this.StaticEnum = baseType.IsStatic;

            foreach (var baseConstructor in baseType.Constructors)
            {
                this.Constructors.Add(new SerializationMethodModel(baseConstructor));
            }

            foreach (var baseField in baseType.Fields)
            {
                this.Fields.Add(new SerializationParameterModel(baseField));
            }

            foreach (var baseGenericArgument in baseType.GenericArguments)
            {
                this.GenericArguments.Add(GetOrAdd(baseGenericArgument));
            }

            foreach (var baseImplementedInterface in baseType.ImplementedInterfaces)
            {
                this.ImplementedInterfaces.Add(GetOrAdd(baseImplementedInterface));
            }

            foreach (var baseMethod in baseType.Methods)
            {
                this.Methods.Add(new SerializationMethodModel(baseMethod));
            }

            foreach (var baseNestedType in baseType.NestedTypes)
            {
                this.NestedTypes.Add(GetOrAdd(baseNestedType));
            }

            foreach (var baseProperty in baseType.Properties)
            {
                this.Properties.Add(new SerializationPropertyModel(baseProperty));
            }
        }

        public static SerializationTypeModel GetOrAdd(BaseTypeModel baseType)
        {
            if (TypeDictionary.ContainsKey(baseType.Name))
            {
                return TypeDictionary[baseType.Name];
            }
            else
            {
                return new SerializationTypeModel(baseType);
            }
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new string NamespaceName { get; set; }

        [DataMember]
        public new SerializationTypeModel BaseType { get; set; }

        [DataMember]
        public new List<SerializationTypeModel> GenericArguments { get; set; }

        [DataMember]
        public new Accessibility AccessLevel { get; set; }

        [DataMember]
        public new IsAbstract AbstractEnum { get; set; }

        [DataMember]
        public new IsStatic StaticEnum { get; set; }

        [DataMember]
        public new IsSealed SealedEnum { get; set; }

        [DataMember]
        public new TypeKind Type { get; set; }

        [DataMember]
        public new List<SerializationTypeModel> ImplementedInterfaces { get; set; }

        [DataMember]
        public new List<SerializationTypeModel> NestedTypes { get; set; }

        [DataMember]
        public new List<SerializationPropertyModel> Properties { get; set; }

        [DataMember]
        public new SerializationTypeModel DeclaringType { get; set; }

        [DataMember]
        public new List<SerializationMethodModel> Methods { get; set; }

        [DataMember]
        public new List<SerializationMethodModel> Constructors { get; set; }

        [DataMember]
        public new List<SerializationParameterModel> Fields { get; set; }

        public static Dictionary<string, SerializationTypeModel> TypeDictionary = new Dictionary<string, SerializationTypeModel>();

    }
}