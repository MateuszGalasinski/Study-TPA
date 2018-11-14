using UILogic.Base;
using UILogic.ViewModel;
using System;
using System.Linq;
using TreeViewConsoleApp.DisplaySupport;
using TreeViewConsoleApp.ReactiveBase;

namespace TreeViewConsoleApp
{
    public class MainView : FrameworkElement
    {
        private readonly MainViewModel _viewModel;

        public MenuDisplay ConsoleDisplay { get; } = new MenuDisplay();

        public MenuItemView ConsoleTreeView { get; } = new MenuItemView();

        public MainView(MainViewModel viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;
            SetBinding("Name");
            ConsoleDisplay.Add(new MenuItem() { Command = _viewModel.GetFilePathCommand, Header = "1. Choose .dll file from filesystem" });
            ConsoleDisplay.Add(new MenuItem() { Command = _viewModel.LoadMetadataCommand, Header = "2. Load .dll assembly" });
            ConsoleDisplay.Add(new MenuItem()
            {
                Command = new RelayCommand(() =>
                {
                    ConsoleTreeView.TreeItems = _viewModel.MetadataTree.ToList();
                    ConsoleTreeView.Display();
                }, () => _viewModel.MetadataTree.ToList()?.Any() == true),
                Header = "3. List assembly of selected .dll"
            });
            ConsoleDisplay.Add(new MenuItem() { Command = new RelayCommand(() => Environment.Exit(0)), Header = "q. Exit" });
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
