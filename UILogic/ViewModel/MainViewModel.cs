using Core.Components;
using ReflectionLoading;
using ReflectionLoading.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UILogic.Base;
using UILogic.Interfaces;
using UILogic.Model;

namespace UILogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly ILogger _logger;
        private Reflector _reflector;
        private ObservableCollection<TreeItem> _metadataTree;
        private readonly object _openSyncLock = new object();
        private bool _isExecuting;
        private string _filePath;

        public IRaiseCanExecuteCommand LoadMetadataCommand { get; }
        public IRaiseCanExecuteCommand GetFilePathCommand { get; }

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }

            private set
            {
                _isExecuting = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
                GetFilePathCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<TreeItem> MetadataTree
        {
            get => _metadataTree;
            private set => SetProperty(ref _metadataTree, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public MainViewModel(IFilePathGetter filePathGetter, ILogger logger)
        {
            _logger = logger;
            _filePathGetter = filePathGetter;
            MetadataTree = new ObservableCollection<TreeItem>();
            LoadMetadataCommand = new RelayCommand(Open, () => !_isExecuting);
            GetFilePathCommand = new RelayCommand(GetFilePath, () => !_isExecuting);
        }

        private void GetFilePath()
        {
            lock (_openSyncLock)
            {
                if (IsExecuting)
                {
                    return;
                }

                IsExecuting = true;
            }

            _logger.Trace($"Reading file path...");
            string filePath = _filePathGetter.GetFilePath();
            if (string.IsNullOrEmpty(filePath) || !filePath.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.Trace($"Selected file was invalid!");
                IsExecuting = false;
                return;
            }

            _logger.Trace($"Read file path: {filePath}");
            FilePath = filePath;
            IsExecuting = false;
        }

        private async void Open()
        {
            lock (_openSyncLock)
            {
                if (IsExecuting)
                {
                    return;
                }

                IsExecuting = true;
            }

            await Task.Run(() =>
            {
                try
                {
                    _logger.Trace("Beginning reflection subroutine...");
                    _reflector = new Reflector(FilePath);
                    _logger.Trace("Reflection subroutine finished successfully!");
                }
                catch (ReflectionLoadException e)
                {
                    _logger.Trace($"ReflectionException thrown, message: {e.Message}");
                }
            }).ConfigureAwait(true);

            if (_reflector == null)
            {
                IsExecuting = false;
                return;
            }

            MetadataTree = new ObservableCollection<TreeItem>() { new AssemblyTreeItem(_reflector.AssemblyModel) };
            _logger.Trace("Successfully loaded root metadata item.");
            IsExecuting = false;
        }
    }
}
