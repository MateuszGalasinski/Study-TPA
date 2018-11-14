using System;
using UILogic.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using UILogic;

namespace TreeViewConsoleApp.DisplaySupport
{
    public class ConsoleItem : IDisplayable
    {
        public TreeItem TreeItem {get; set;}
        public bool IsExpanded {get; set;}
        public int Spacing {get; set;}
        public int Id { get; set; }
        //public string Header { get; set; }
        public ConsoleItem(TreeItem _treeItem, int _spacing)
        {
            TreeItem = _treeItem;
            Spacing = _spacing;
            IsExpanded = false;
        }

        public ObservableCollection<TreeItem> Expand()
        {
            IsExpanded = true;
            TreeItem.IsExpanded = true;
            return TreeItem.Children();
        }

        public ICommand Command { get; set; }

        public void Display()
        {
            Console.Write(new string(' ', Spacing * 3));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Id}. ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(IsExpanded ? "(-) " : "(+) ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{TreeItem.TypeName} ");
            Console.ResetColor();
            Console.WriteLine(TreeItem.Name);
        }
    }
}
