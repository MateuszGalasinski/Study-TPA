﻿using SharedUILogic.Base;
using SharedUILogic.ViewModel;
using System;
using System.Linq;
using TreeViewConsoleApp.DisplaySupport;
using TreeViewConsoleApp.ReactiveBase;

namespace TreeViewConsoleApp
{
    public class MainView : FrameworkElement
    {
        private readonly IMainViewModel _viewModel;

        public ConsoleDisplay ConsoleDisplay { get; } = new ConsoleDisplay();

        public ConsoleTreeView ConsoleTreeView { get; } = new ConsoleTreeView();

        public MainView(IMainViewModel viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;
            SetBinding("Name");
            ConsoleDisplay.Add(new ConsoleItem() { Command = _viewModel.GetFilePathCommand, Header = "1. Choose .dll file from filesystem" });
            ConsoleDisplay.Add(new ConsoleItem() { Command = _viewModel.LoadMetadataCommand, Header = "2. Load .dll assembly" });
            ConsoleDisplay.Add(new ConsoleItem()
            {
                Command = new RelayCommand(() =>
                {
                    ConsoleTreeView.TreeItems = _viewModel.TreeItems;
                    ConsoleTreeView.Display();
                },() => _viewModel.TreeItems?.Any() == true ),
                Header = "3. List assembly of selected .dll"
            });
            ConsoleDisplay.Add(new ConsoleItem() { Command = new RelayCommand(() => Environment.Exit(0)), Header = "q. Exit" });
        }

        public void List()
        {
            while (true)
            {
                ConsoleDisplay.Print();
                ConsoleDisplay.ReceiveInput();
            }
        }
    }
}