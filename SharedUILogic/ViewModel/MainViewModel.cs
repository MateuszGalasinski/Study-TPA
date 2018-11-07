using Core.Components;
using Core.Model;
using SharedUILogic.Base;
using SharedUILogic.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedUILogic.ViewModel
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly ILogger _logger;
        private readonly IStoreProvider _metadataProvider;
        private readonly IMapper<AssemblyMetadataStore, TreeItem> _mapper;

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                SetProperty(ref _filePath, value);
                LoadMetadataCommand.RaiseCanExecuteChanged();
            }

        }

        public ICommand GetFilePathCommand { get; }

        public ICanExecuteCommand LoadMetadataCommand { get; }

        private List<TreeItem> _treeItems;

        public List<TreeItem> TreeItems
        {
            get => _treeItems;
            set => SetProperty(ref _treeItems, value);
        }

        public MainViewModel(
            IFilePathGetter filePathGetter,
            ILogger logger,
            IStoreProvider metadataProvider,
            IMapper<AssemblyMetadataStore, TreeItem> mapper)
        {
            _filePathGetter = filePathGetter;
            _logger = logger;
            _metadataProvider = metadataProvider;
            _mapper = mapper;
            GetFilePathCommand = new RelayCommand(GetFilePath);
            LoadMetadataCommand = new SimpleAsyncCommand(LoadMetadata, () => File.Exists(_filePath));
            TreeItems = new List<TreeItem>();
        }

        private void GetFilePath()
        {
            FilePath = _filePathGetter.GetFilePath();
        }

        private async Task LoadMetadata()
        {
            Task loadTreeTask = new Task(() =>
            {
                AssemblyMetadataStore storage = _metadataProvider.GetAssemblyMetadataStore(FilePath);
                TreeItems = new List<TreeItem>() { _mapper.Map(storage) };
            });
            loadTreeTask.Start();
            await loadTreeTask.ConfigureAwait(false);
        }
    }
}