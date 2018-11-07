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

        protected const string FilePath = "somePath";

        [SetUp]
        public void Given()
        {
            _filePathGetterMock = new Mock<IFilePathGetter>(MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(MockBehavior.Strict);
            _storeProviderMock = new Mock<IStoreProvider>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper<AssemblyMetadataStore, TreeItem>>(MockBehavior.Strict);

            _context = new MainViewModel(
                _filePathGetterMock.Object,
                _loggerMock.Object,
                _storeProviderMock.Object,
                _mapperMock.Object);
        }

        public void With_FilePathGetter()
        {
            _filePathGetterMock.Setup(m => m.GetFilePath())
                .Returns(FilePath);
        }
    }
}
