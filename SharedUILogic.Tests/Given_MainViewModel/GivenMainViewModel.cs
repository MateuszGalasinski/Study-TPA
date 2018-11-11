using Core.Components;
using Core.Model;
using Moq;
using NUnit.Framework;
using SharedUILogic.Model;
using SharedUILogic.ViewModel;
using System.Collections.Generic;

namespace SharedUILogic.Tests.Given_MainViewModel
{
    [TestFixture]
    public class GivenMainViewModel
    {
        protected MainViewModel _context;
        protected Mock<IFilePathGetter> _filePathGetterMock;
        protected Mock<ILogger> _loggerMock;
        protected Mock<IDataSource> _storeProviderMock;
        protected Mock<IMapper<AssemblyMetadataStore, TreeItem>> _mapperMock;

        protected const string FilePath = "somePath";
        protected AssemblyMetadataStore Store { get; set; }
        protected TreeItem TreeRoot { get; set; }

        [SetUp]
        public void Given()
        {
            _filePathGetterMock = new Mock<IFilePathGetter>(MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(MockBehavior.Strict);
            _storeProviderMock = new Mock<IDataSource>(MockBehavior.Strict);
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

        public void With_AssemblyMetadataStore()
        {
            Store = new AssemblyMetadataStore(new AssemblyMetadata()
            {
                Id = "id",
                Name = "name",
                Namespaces = new List<NamespaceMetadata>()
            });
        }

        public void With_TreeRoot()
        {
            TreeRoot = new TreeItem("Name", false);
        }

        public void With_Mapper()
        {
            _mapperMock.Setup(m => m.Map(It.IsAny<AssemblyMetadataStore>()))
                .Returns(TreeRoot);
        }

        public void With_StoreProvider()
        {
            _storeProviderMock.Setup(m => m.GetAssemblyMetadataStore(It.IsAny<string>()))
                .Returns(Store);
        }
    }
}
