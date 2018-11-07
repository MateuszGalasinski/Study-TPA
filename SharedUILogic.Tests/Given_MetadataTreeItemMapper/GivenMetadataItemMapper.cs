using Core.Constants;
using Core.Model;
using NUnit.Framework;
using SharedUILogic.Services;
using System;
using System.Collections.Generic;

namespace SharedUILogic.Tests.Given_MetadataTreeItemMapper
{
    [TestFixture]
    public class GivenMetadataItemMapper
    {
        protected TreeItemMapper _context;
        protected AssemblyMetadata _assemblyMetadata;
        protected AssemblyMetadataStore _storage;

        protected const string _assemblyName = "TestAssembly";
        protected const string _namespaceName = "TestNamespace";
        protected const string _typeName = "TestType";
        protected const string _secondTypeName = "2TestType";
        protected const string _thirdTypeName = "3TestType";
        protected const string _propertyName = "TestProperty";
        protected const string _methodName = "TestMethod";
        protected const string _parameterName = "TestParameter";
        protected const Accessibility _typeAccessLevel = Accessibility.Public;
        protected const Accessibility _methodAccessLevel = Accessibility.Public;

        [SetUp]
        public void Given()
        {
            _context = new TreeItemMapper();
        }

        protected void With_AssemblyMetadata()
        {
            _assemblyMetadata = new AssemblyMetadata()
            {
                Id = _assemblyName,
                Name = _assemblyName,
                Namespaces = new List<NamespaceMetadata>()
            };

            _storage = new AssemblyMetadataStore(_assemblyMetadata);
        }


        protected void With_NamespaceMetaData()
        {
            NamespaceMetadata namespaceData = new NamespaceMetadata()
            {
                Id = _namespaceName,
                Name = _namespaceName,
                Types = new List<TypeMetadata>()
            };

            (_assemblyMetadata.Namespaces as List<NamespaceMetadata>).Add(namespaceData);
            _storage.NamespacesDictionary.Add(namespaceData.Id, namespaceData);
        }

        protected void With_TypeMetaData()
        {
            NamespaceMetadata namespaceMetadata = _storage.NamespacesDictionary[_namespaceName];
            TypeMetadata typeMetadata = CreateSimpleTypeMetadata(_typeName);
            (namespaceMetadata.Types as List<TypeMetadata>).Add(typeMetadata);
            _storage.TypesDictionary.Add(typeMetadata.Id, typeMetadata);
        }

        protected void With_PropertyMetadata()
        {
            TypeMetadata typeMetadata = _storage.TypesDictionary[_typeName];
            typeMetadata.Kind = TypeKind.ClassType;

            TypeMetadata propertyTypeMetadata = CreateSimpleTypeMetadata(_secondTypeName);
            PropertyMetadata propertyMetadata = new PropertyMetadata()
            {
                Id = _propertyName,
                Name = _propertyName,
                TypeMetadata = propertyTypeMetadata
            };
            (typeMetadata.Properties as List<PropertyMetadata>).Add(propertyMetadata);
            _storage.PropertiesDictionary.Add(propertyMetadata.Id, propertyMetadata);
            _storage.TypesDictionary.Add(propertyTypeMetadata.Id, propertyTypeMetadata);
        }

        protected void With_VoidMethodMetadata(IEnumerable<ParameterMetadata> parameters)
        {
            TypeMetadata typeMetadata = _storage.TypesDictionary[_typeName];
            typeMetadata.Kind = TypeKind.ClassType;

            MethodMetadata methodMetadata = new MethodMetadata()
            {
                Id = _methodName,
                Name = _methodName,
                GenericArguments = new List<TypeMetadata>(),
                Modifiers = new Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual>(
                    _methodAccessLevel,
                    default(IsAbstract),
                    default(IsStatic),
                    default(IsVirtual)),
                ReturnType = null,
                Parameters = parameters
            };

            foreach (ParameterMetadata parameterMetadataDto in parameters)
            {
                _storage.ParametersDictionary.Add(parameterMetadataDto.Id, parameterMetadataDto);   
                _storage.TypesDictionary.Add(parameterMetadataDto.TypeMetadata.Id, parameterMetadataDto.TypeMetadata);
            }

            (typeMetadata.Methods as List<MethodMetadata>).Add(methodMetadata);
            _storage.MethodsDictionary.Add(methodMetadata.Id, methodMetadata);
        }

        protected TypeMetadata CreateSimpleTypeMetadata(string typeName)
        {
            return new TypeMetadata
            {
                Id = typeName,
                Name = typeName,
                NamespaceName = _namespaceName,
                Modifiers = new Tuple<Accessibility, IsSealed, IsAbstract>(
                    _typeAccessLevel,
                    default(IsSealed),
                    default(IsAbstract)),
                Kind = default(TypeKind),
                Attributes = new List<Attribute>(),
                Properties = new List<PropertyMetadata>(),
                Constructors = new List<MethodMetadata>(),
                GenericArguments = new List<TypeMetadata>(),
                ImplementedInterfaces = new List<TypeMetadata>(),
                Methods = new List<MethodMetadata>(),
                NestedTypes = new List<TypeMetadata>()
            };
        }
    }
}
