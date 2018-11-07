using System.Windows.Input;

namespace TreeViewConsoleApp.DisplaySupport
{
    public class ConsoleItem
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }
    }
}
