using Core.Components;
using Core.Model;
using Moq;
using NUnit.Framework;
using SharedUILogic.Model;
using SharedUILogic.ViewModel;

namespace SharedUILogic.Tests.Given_MainViewModel
{
    [TestFixture]
    public class GivenMainViewModel
    {
        protected MainViewModel _context;
        protected Mock<IFilePathGetter> _filePathGetterMock;
        protected Mock<ILogger> _loggerMock;
        protected Mock<IStoreProvider> _storeProviderMock;
        protected Mock<IMapper<AssemblyMetadataStore, TreeItem>> _mapperMock;

        [SetUp]
        public void Given()
        {
            _context = new MainViewModel(
                _filePathGetterMock.Object,
                _loggerMock.Object,
                _storeProviderMock.Object,
                _mapperMock.Object);
        }
    }
}
