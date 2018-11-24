using Core.Constants;
using FluentAssertions;
using NUnit.Framework;
using ReflectionLoading;
using ReflectionLoading.Models;
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

        [SetUp]
        public void SetUp()
        {
            string testFilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"Resources\TestLibrary.dll");

            _reflector = new Reflector(testFilePath);
        }

        [Test]
        public void When_ReflectorConstructorCalled_NumberOfNamespacesShouldBeFour()
        {
            _reflector.AssemblyModel.NamespaceModels.Count.Should().Be(4);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfTypes()
        {
            List<TypeModel> baseNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == BaseNamespace).Types;
            baseNamespace.Count.Should().Be(2);

            List<TypeModel> firstNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == FirstNamespace).Types;
            firstNamespace.Count.Should().Be(4);

            List<TypeModel> secondNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == SecondNamespace).Types;
            secondNamespace.Count.Should().Be(1);

            List<TypeModel> thirdNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == ThirdNamespace).Types;
            thirdNamespace.Count.Should().Be(3);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfPropertiesInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == ThirdNamespace).Types.Where(t => t.Name == "CircularA").ToList();
            classes.First().Properties.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfMethodsInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.Name == "CModel").ToList();
            classes.First().Methods.Count.Should().Be(2);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfFieldsInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == BaseNamespace).Types.Where(t => t.Name == "BModel").ToList();
            classes.First().Fields.Count.Should().Be(3);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectAbstractClasses()
        {
            List<TypeModel> abstractClasses = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types
                .Where(t => t.IsAbstract == IsAbstract.Abstract).ToList();
            abstractClasses.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfStaticClasses()
        {
            List<TypeModel> statics = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == BaseNamespace).Types
                .Where(t => t.IsStatic == IsStatic.Static).ToList();
            statics.Count.Should().Be(1);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfClassesThatInherits()
        {
            List<TypeModel> classesThatInherits = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.BaseType != null).ToList();
            classesThatInherits.Count.Should().Be(2);
        }

        [Test]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfGenericArguments()
        {
            List<TypeModel> genericClasses = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == SecondNamespace).Types.Where(t => t.GenericArguments != null)
                .ToList();
            genericClasses.Count.Should().Be(1);
            genericClasses.First().GenericArguments.Count.Should().Be(2);
            genericClasses.First().GenericArguments.Should().Contain(i => i.Name == "TKey");
            genericClasses.First().GenericArguments.Should().Contain(i => i.Name == "TValue");

        }
    }
}
