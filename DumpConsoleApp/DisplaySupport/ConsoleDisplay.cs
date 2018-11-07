using System;
using System.Collections.Generic;
using System.Linq;

namespace DumpConsoleApp.Framework_Elements
{
    public class ConsoleDisplay
    {
        private readonly List<ConsoleItem> _menuItems = new List<ConsoleItem>();

        public void Print(bool clearScreen = true)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            foreach (ConsoleItem menuItem in Items)
            {
                Console.WriteLine(menuItem.Header);
            }
        }

        public void ReceiveInput()
        {
            bool notDone = true;
            while (notDone)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                foreach (ConsoleItem menuItem in _menuItems.Where(item => item.Command != null && item.Header.Substring(0, 1) == key.KeyChar.ToString()))
                {
                    menuItem.Command.Execute(null);
                    notDone = false;
                }
            }
        }

        public void Add(ConsoleItem consoleItem)
        {
            _menuItems.Add(consoleItem);
        }

        public IEnumerable<ConsoleItem> Items => _menuItems;
    }
}
