using Core.Components;
using Moq;
using NUnit.Framework;
using System;
using System.Reflection;
using UILogic.Interfaces;
using UILogic.ViewModel;

namespace SharedUILogic.Tests.Given_MainViewModel
{
    [TestFixture]
    public class GivenMainViewModel
    {
        protected MainViewModel _context;
        protected Mock<IFilePathGetter> _filePathGetterMock;
        protected Mock<ILogger> _loggerMock;

        protected string FilePath;

        [SetUp]
        public void Given()
        {
            _filePathGetterMock = new Mock<IFilePathGetter>(MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(MockBehavior.Strict);

            _context = new MainViewModel(
                _filePathGetterMock.Object,
                _loggerMock.Object);
        }

        public void With_FilePathGetter()
        {
            _filePathGetterMock.Setup(m => m.GetFilePath())
                .Returns(GetAssemblyPath());

            FilePath = GetAssemblyPath(); // for assertion purposes
        }

        public void With_Logger()
        {
            _loggerMock.Setup(m => m.Trace(It.IsAny<string>()));
        }

        // to get any file that exists
        private string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return path;
        }
    }
}
