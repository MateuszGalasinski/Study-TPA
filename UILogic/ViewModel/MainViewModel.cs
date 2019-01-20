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
        private ObservableCollection<TreeItem> _metadataTree;
        private readonly object _openSyncLock = new object();
        private bool _isExecuting;
        private string _filePath;
        private AssemblyManager _assemblyManager;

        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }

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

        public AssemblyManager AssemblyManager
        {
            get => _assemblyManager;
            set => SetProperty(ref _assemblyManager, value);
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
            LoadMetadataCommand = new RelayCommand(OpenDll, () => !_isExecuting && !string.IsNullOrWhiteSpace(FilePath));
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

            Logger.Trace("Reading file path...");
            string filePath = _filePathGetter.GetFilePath();
            if (!filePath?.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase) == true)
            {
                EndAsynchronousCommand("Selected file was invalid!");
                return;
            }
            FilePath = filePath;
            EndAsynchronousCommand($"Read file path: {filePath}");
        }

        private async void OpenDll()
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

            if (AssemblyManager?.AssemblyModel == null)
            {
                EndAsynchronousCommand("Loaded Assembly Model was null.");
                return;
            }

            MetadataTree = new ObservableCollection<TreeItem>() { new AssemblyTreeItem(AssemblyManager.AssemblyModel) };
            EndAsynchronousCommand("Successfully loaded root metadata item.");
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
            EndAsynchronousCommand("Successfully loaded root metadata item.");
        }

        public async void SaveData()
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
                    Logger.Trace("Beginning save subroutine...");
                    AssemblyManager.SaveAssembly(AssemblyManager.AssemblyModel);
                    Logger.Trace("Save subroutine finished successfully!");
                }
                catch (Exception e)
                {
                    Logger.Trace($"Exception thrown, message: {e.Message}");
                }
            }).ConfigureAwait(true);

            EndAsynchronousCommand($"Save completed");
        }

        private void EndAsynchronousCommand(string logMessage)
        {
            Logger.Trace(logMessage);
            IsExecuting = false;
        }
    }
}
