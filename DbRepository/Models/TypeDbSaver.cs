using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BaseCore.Enums;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class TypeDbSaver
    {
        private TypeDbSaver()
        {
            MethodGenericArguments = new HashSet<MethodDbSaver>();
            TypeGenericArguments = new HashSet<TypeDbSaver>();
            TypeImplementedInterfaces = new HashSet<TypeDbSaver>();
            TypeNestedTypes = new HashSet<TypeDbSaver>();
        }

        private TypeDbSaver(TypeBase baseType)
        {
            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.IsAbstract = baseType.IsAbstract;
            this.AccessLevel = baseType.AccessLevel;
            this.IsSealed = baseType.IsSealed;
            this.IsStatic = baseType.IsStatic;

            Constructors = baseType.Constructors?.Select(t => new MethodDbSaver(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodDbSaver(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertyDbSaver(t)).ToList();

        }

        public static TypeDbSaver GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (TypeDictionary.ContainsKey(baseType.Name))
                {
                    return TypeDictionary[baseType.Name];
                }
                else
                {
                    return new TypeDbSaver(baseType);
                }
            }
            else
                return null;
        }

        [Key, StringLength(150)]
        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeDbSaver BaseType { get; set; }

        public List<TypeDbSaver> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsSealed IsSealed { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeDbSaver> ImplementedInterfaces { get; set; }

        public List<TypeDbSaver> NestedTypes { get; set; }

        public List<PropertyDbSaver> Properties { get; set; }

        public TypeDbSaver DeclaringType { get; set; }

        public List<MethodDbSaver> Methods { get; set; }

        public List<MethodDbSaver> Constructors { get; set; }

        public static Dictionary<string, TypeDbSaver> TypeDictionary = new Dictionary<string, TypeDbSaver>();

        public ICollection<TypeDbSaver> TypeBaseTypes { get; set; }

        public ICollection<TypeDbSaver> TypeDeclaringTypes { get; set; }

        [InverseProperty("GenericArguments")]
        public ICollection<MethodDbSaver> MethodGenericArguments { get; set; }

        [InverseProperty("GenericArguments")]
        public ICollection<TypeDbSaver> TypeGenericArguments { get; set; }

        [InverseProperty("ImplementedInterfaces")]
        public ICollection<TypeDbSaver> TypeImplementedInterfaces { get; set; }

        [InverseProperty("NestedTypes")]
        public ICollection<TypeDbSaver> TypeNestedTypes { get; set; }
    }
}
