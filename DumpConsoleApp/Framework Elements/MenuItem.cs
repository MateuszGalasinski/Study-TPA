using System.Windows.Input;

namespace DumpConsoleApp.Framework_Elements
{
    public class MenuItem
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }
    }
}
