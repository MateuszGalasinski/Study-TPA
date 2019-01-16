using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Logic.Enums;
using Logic.Models;
using NUnit.Framework;
using ReflectionLoading;

namespace ReflectionTests
{
    public class ApllicationReflectorTetss
    {
        private string FilePath;
        private const string FirstNamespace = "TPA.ApplicationArchitecture.Data";
        private const string SecondNamespace = "TPA.ApplicationArchitecture.Data.CircularReference";
        private Reflector _reflection;
        private AssemblyModel assemblyModel;

        [SetUp]
        public void Initialize()
        {

            string FilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"Resources\TPA.ApplicationArchitecture.dll");

            _reflection = new Reflector();
            assemblyModel = _reflection.LoadAssembly(FilePath);
        }

        [Test]
        public void Reflector_NumberOfLoadedNamespaces_ShouldBeOk()
        {
            int expected = 4;
            int actual = assemblyModel.NamespaceModels.Count;
            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedTypes_ShouldBeOk()
        {
            int firstExpected = 12;
            int firstActual = assemblyModel.NamespaceModels.Find(n => n.Name == FirstNamespace).Types.Count;
            int secondExpected = 2;
            int secondActual = assemblyModel.NamespaceModels.Find(n => n.Name == SecondNamespace).Types.Count;

            firstActual.Should().Be(firstExpected);
            secondActual.Should().Be(secondExpected);
        }

        [Test]
        public void Reflector_NumberOfLoadedFieldsInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "GenericClass <T>").ToList().First().Fields.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedPropertiesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "InnerClass").ToList().First().Properties.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedMethodsInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "AbstractClass").ToList().First().Methods.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedGenericArguments_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.GenericArguments.Count != 0).ToList().Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedConstructorsInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "Linq2SQL").ToList().First().Constructors.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedParametersInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .First(n => n.Name == "AbstractClass").Methods
                .Find(m => m.Name == "set_Property1").Parameters.ToList().Count;

            actual.Should().Be(expected);

        }


        [Test]
        public void Reflector_NumberOfLoadedImplementedInterfacesInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "Enum").ToList().First().ImplementedInterfaces.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedAttributesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .First(t => t.Name == "ClassWithAttribute").Attributes.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Reflector_NumberOfLoadedEnums_ShouldBeOk()
        {
            int expected = 1;
            int actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .Where(t => t.Type == TypeKind.EnumType).ToList().Count;

            actual.Should().Be(expected);

        }

        [Test]
        public void Reflector_CorrectNameOfLoadedDerivedClassesInClass_ShouldBeOk()
        {
            string expected = "AbstractClass";
            string actual = assemblyModel.NamespaceModels
                .Find(n => n.Name == FirstNamespace).Types
                .First(t => t.Name == "DerivedClass").BaseType.Name;

            actual.Should().Be(expected);
        }
    }
}