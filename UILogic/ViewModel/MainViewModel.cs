﻿using Logic.Components;
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
        private AssemblyManager _assemblyManager;
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

        public MainViewModel(IFilePathGetter filePathGetter, ILogger logger, AssemblyManager assemblyManager)
        {
            _logger = logger;
            _filePathGetter = filePathGetter;
            _assemblyManager = assemblyManager;
            MetadataTree = new ObservableCollection<TreeItem>();
            LoadMetadataCommand = new RelayCommand(Open, () => !_isExecuting);
            GetFilePathCommand = new RelayCommand(GetFilePath, () => !_isExecuting);
            SaveDataCommand = new RelayCommand(SavaData, () => !_isExecuting);
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
            if (string.IsNullOrEmpty(filePath) 
                || !(filePath.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase) 
                || filePath.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase)))
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
                    if (FilePath.EndsWith(".xml"))
                    {
                        _assemblyManager.LoadAssemblyFromStorage(FilePath);
                    }
                    else if (FilePath.EndsWith(".dll"))
                    {
                        _assemblyManager.LoadAssemblyFromLibrary(FilePath);
                    }
                    _logger.Trace("Reflection subroutine finished successfully!");
                }
                catch (ReflectionLoadException e)
                {
                    _logger.Trace($"ReflectionException thrown, message: {e.Message}");
                }
            }).ConfigureAwait(true);

            if (_assemblyManager == null)
            {
                IsExecuting = false;
                return;
            }

            MetadataTree = new ObservableCollection<TreeItem>() { new AssemblyTreeItem(_assemblyManager.AssemblyModel) };
            _logger.Trace("Successfully loaded root metadata item.");
            IsExecuting = false;
        }

        public void SavaData()
        {
            // TODO fix error connected with selecting .dll to serialize
            _serializationFilePath = _filePathGetter.GetFilePath();
            if (_serializationFilePath.EndsWith(".xml"))
            {
                try
                {
                    _assemblyManager.SaveAssembly(_assemblyManager.AssemblyModel, _serializationFilePath);
                }
            catch (Exception e)
                {
                    _logger.Trace($"Exception thrown, message: {e.Message}");
                }
                _logger.Trace($"Serialization completed");
            }
            else
            {
                _logger.Trace($"Serialization error");
            }
        }
    }
}
