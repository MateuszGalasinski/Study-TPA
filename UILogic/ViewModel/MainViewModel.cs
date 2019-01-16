using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Logic.Components;
using ReflectionLoading;
using ReflectionLoading.Exceptions;
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
        public IRaiseCanExecuteCommand LoadSerialized { get; }
        public IRaiseCanExecuteCommand GetFilePathCommand { get; }
        public IRaiseCanExecuteCommand SaveDataCommand { get; }

        public bool IsExecuting
        {
            get => _isExecuting;

            private set
            {
                _isExecuting = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
                LoadSerialized.RaiseCanExecuteChanged();
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
            LoadMetadataCommand = new RelayCommand(OpenDLL, () => !_isExecuting && !string.IsNullOrWhiteSpace(FilePath));
            LoadSerialized = new RelayCommand(OpenFromStorage, () => !_isExecuting);
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
                && !filePath.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
            {
                Logger.Trace($"Selected file was invalid!");
                IsExecuting = false;
                return;
            }
            Logger.Trace($"Read file path: {filePath}");
            FilePath = filePath;
            IsExecuting = false;
        }

        private async void OpenDLL()
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
                    if (FilePath.EndsWith(".dll"))
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

        private async void OpenFromStorage()
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
                    Logger.Trace("Beginning deserialization subroutine...");
                    AssemblyManager.LoadAssemblyFromStorage();
                    Logger.Trace("Deserialization subroutine finished successfully!");
                }
                catch (Exception e)
                {
                    Logger.Trace($"Exception thrown, message: {e.Message}");
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
            try
            {
                AssemblyManager.SaveAssembly(AssemblyManager.AssemblyModel);
            }
            catch (Exception e)
            {
                Logger.Trace($"Exception thrown, message: {e.Message}");
            }
            Logger.Trace($"Serialization completed");
        }
    }
}
