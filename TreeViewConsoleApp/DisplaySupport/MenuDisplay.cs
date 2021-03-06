﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeViewConsoleApp.DisplaySupport
{
    public class MenuDisplay
    {
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        public void Print(bool clearScreen = true)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            foreach (MenuItem menuItem in Items)
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
                foreach (MenuItem menuItem in _menuItems.Where(item =>
                    item.Command != null && item.Header.Substring(0, 1) == key.KeyChar.ToString()))
                {
                    if (menuItem.Command.CanExecute(null))
                    {
                        menuItem.Command.Execute(null);
                    }

                    notDone = false;
                }
            }
        }

        public void Add(MenuItem consoleItem)
        {
            _menuItems.Add(consoleItem);
        }

        public IEnumerable<MenuItem> Items => _menuItems;
    }
}
