using System;
using System.Linq;
using TreeViewConsoleApp.DisplaySupport;
using TreeViewConsoleApp.ReactiveBase;
using UILogic.Base;
using UILogic.ViewModel;

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
            ConsoleDisplay.Add(new MenuItem() { Command = _viewModel.GetFilePathCommand, Header = "1. Choose .dll file from filesystem" });
            ConsoleDisplay.Add(new MenuItem() { Command = _viewModel.LoadMetadataCommand, Header = "2. Load .dll or .xml assembly" });
            //  TODO view message saying "File selected to serialize is wrong"
            ConsoleDisplay.Add(new MenuItem() { Command = _viewModel.SaveDataCommand, Header = "3. Serialize loaded data" });
            ConsoleDisplay.Add(new MenuItem()
            {
                Command = new RelayCommand(() =>
                {
                    ConsoleTreeView.TreeItems = _viewModel.MetadataTree.ToList();
                    ConsoleTreeView.Display();
                }, () => _viewModel.MetadataTree.ToList()?.Any() == true),
                Header = "4. List assembly of selected .dll or .xml"
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
