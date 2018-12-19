using Logic.Components;
using ReflectionLoading;
using ReflectionLoading.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using UILogic.Base;
using UILogic.Interfaces;
using UILogic.Model;

namespace UILogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        public AssemblyManager AssemblyManager { get; set; }
        private ObservableCollection<TreeItem> _metadataTree;
        private readonly object _openSyncLock = new object();
        private bool _isExecuting;
        private string _filePath;
        private string _serializationFilePath;

        public IRaiseCanExecuteCommand LoadMetadataCommand { get; }
        public IRaiseCanExecuteCommand GetFilePathCommand { get; }
        public IRaiseCanExecuteCommand SaveDataCommand { get; }

        public bool IsExecuting
        {
            get => _isExecuting;

            private set
            {
                _isExecuting = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
                GetFilePathCommand.RaiseCanExecuteChanged();
                SaveDataCommand.RaiseCanExecuteChanged();
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

        public MainViewModel(IFilePathGetter filePathGetter, AssemblyManager assemblyManager)
        {
            _filePathGetter = filePathGetter;
            AssemblyManager = assemblyManager;
            MetadataTree = new ObservableCollection<TreeItem>();
            LoadMetadataCommand = new RelayCommand(Open, () => !_isExecuting && !string.IsNullOrWhiteSpace(FilePath));
            GetFilePathCommand = new RelayCommand(GetFilePath, () => !_isExecuting);
            SaveDataCommand = new RelayCommand(SaveData, () => !_isExecuting && AssemblyManager?.AssemblyModel != null);
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

            Logger.Trace($"Reading file path...");
            string filePath = _filePathGetter.GetFilePath();
            if (string.IsNullOrEmpty(filePath) 
                || !(filePath.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase) 
                || filePath.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase)))
            {
                Logger.Trace($"Selected file was invalid!");
                IsExecuting = false;
                return;
            }
            Logger.Trace($"Read file path: {filePath}");
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
                    Logger.Trace("Beginning reflection subroutine...");
                    if (FilePath.EndsWith(".xml"))
                    {
                        AssemblyManager.LoadAssemblyFromStorage(FilePath);
                    }
                    else if (FilePath.EndsWith(".dll"))
                    {
                        AssemblyManager.LoadAssemblyFromLibrary(FilePath);
                    }
                    Logger.Trace("Reflection subroutine finished successfully!");
                }
                catch (ReflectionLoadException e)
                {
                    Logger.Trace($"ReflectionException thrown, message: {e.Message}");
                }
            }).ConfigureAwait(true);

            if (AssemblyManager == null)
            {
                IsExecuting = false;
                return;
            }

            MetadataTree = new ObservableCollection<TreeItem>() { new AssemblyTreeItem(AssemblyManager.AssemblyModel) };
            Logger.Trace("Successfully loaded root metadata item.");
            IsExecuting = false;
        }

        public void SaveData()
        {
            // TODO fix error connected with selecting .dll to serialize
            _serializationFilePath = _filePathGetter.GetFilePath();
            if (_serializationFilePath.EndsWith(".xml"))
            {
                try
                {
                    AssemblyManager.SaveAssembly(AssemblyManager.AssemblyModel, _serializationFilePath);
                }
            catch (Exception e)
                {
                    Logger.Trace($"Exception thrown, message: {e.Message}");
                }
                Logger.Trace($"Serialization completed");
            }
            else
            {
                Logger.Trace($"Serialization error");
            }
        }
    }
}
