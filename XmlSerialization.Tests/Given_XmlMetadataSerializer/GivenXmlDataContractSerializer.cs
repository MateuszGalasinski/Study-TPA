using NUnit.Framework;
using ReflectionLoading.Models;
using System;
using System.IO;
using System.Reflection;

namespace XmlSerialization.Tests.Given_XmlMetadataSerializer
{
    [TestFixture]
    public class GivenXmlDataContractSerializer
    {
        protected XmlDataContractSerializer _context;
        protected string FilePath;
        protected string ResultPath;
        protected AssemblyModel Root;

        [SetUp]
        public void Given()
        {
            _context = new XmlDataContractSerializer();
        }

        protected void With_FilePathFromResources()
        {
            FilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"Resources\TestLibrary.dll");
        }

        protected void With_ResultFilePath(string resultFileName)
        {
            ResultPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                resultFileName);
        }

        protected void With_AssemblyFromFilePath()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(FilePath);
            TypeModel.TypeDictionary.Clear();
            Root = new AssemblyModel(assembly);
        }
    }
}
