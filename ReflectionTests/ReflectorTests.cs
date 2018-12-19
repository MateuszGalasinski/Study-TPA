using FluentAssertions;
using Logic.Enums;
using Logic.Models;
using NUnit.Framework;
using ReflectionLoading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReflectionTests
{
    public class ReflectorTests
    {
        public const string BaseNamespace = "TestLibrary";
        public const string FirstNamespace = "TestLibrary.FirstNamespace";
        private const string SecondNamespace = "TestLibrary.SecondNamespace";
        private const string ThirdNamespace = "TestLibrary.ThirdNamespace";
        private Reflector _reflector;
        private AssemblyModel assemblyModel;

        [SetUp]
        public void SetUp()
        {
            string testFilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"Resources\TestLibrary.dll");

            _reflector = new Reflector();
            assemblyModel = _reflector.LoadAssembly(testFilePath);
        }

        [Test]
        public void When_ReflectorConstructorCalled_NumberOfNamespacesShouldBeFour()
        {
            assemblyModel.NamespaceModels.Count.Should().Be(4);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfTypes()
        {
            List<TypeModel> baseNamespace = assemblyModel.NamespaceModels.Find(t => t.Name == BaseNamespace).Types;
            baseNamespace.Count.Should().Be(2);

            List<TypeModel> firstNamespace = assemblyModel.NamespaceModels.Find(t => t.Name == FirstNamespace).Types;
            firstNamespace.Count.Should().Be(4);

            List<TypeModel> secondNamespace = assemblyModel.NamespaceModels.Find(t => t.Name == SecondNamespace).Types;
            secondNamespace.Count.Should().Be(1);

            List<TypeModel> thirdNamespace = assemblyModel.NamespaceModels.Find(t => t.Name == ThirdNamespace).Types;
            thirdNamespace.Count.Should().Be(3);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfPropertiesInClass()
        {
            List<TypeModel> classes = assemblyModel.NamespaceModels
                .Find(t => t.Name == ThirdNamespace).Types.Where(t => t.Name == "CircularA").ToList();
            classes.First().Properties.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfMethodsInClass()
        {
            List<TypeModel> classes = assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.Name == "CModel").ToList();
            classes.First().Methods.Count.Should().Be(2);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfFieldsInClass()
        {
            List<TypeModel> classes = assemblyModel.NamespaceModels
                .Find(t => t.Name == BaseNamespace).Types.Where(t => t.Name == "BModel").ToList();
            classes.First().Fields.Count.Should().Be(3);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectAbstractClasses()
        {
            List<TypeModel> abstractClasses = assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types
                .Where(t => t.IsAbstract == IsAbstract.Abstract).ToList();
            abstractClasses.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfStaticClasses()
        {
            List<TypeModel> statics = assemblyModel.NamespaceModels
                .Find(t => t.Name == BaseNamespace).Types
                .Where(t => t.IsStatic == IsStatic.Static).ToList();
            statics.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfClassesThatInherits()
        {
            List<TypeModel> classesThatInherits = assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.BaseType != null).ToList();
            classesThatInherits.Count.Should().Be(2);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfGenericArguments()
        {
            List<TypeModel> genericClasses = assemblyModel.NamespaceModels
                .Find(t => t.Name == SecondNamespace).Types.Where(t => t.GenericArguments != null)
                .ToList();
            genericClasses.Count.Should().Be(1);
            genericClasses.First().GenericArguments.Count.Should().Be(2);
            genericClasses.First().GenericArguments.Should().Contain(i => i.Name == "TKey");
            genericClasses.First().GenericArguments.Should().Contain(i => i.Name == "TValue");

        }
    }
}
