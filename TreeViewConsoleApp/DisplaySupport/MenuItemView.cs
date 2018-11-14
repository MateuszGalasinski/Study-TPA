using System;
using System.Collections.Generic;
using System.Linq;
using UILogic.Model;

namespace TreeViewConsoleApp.DisplaySupport
{
    public class MenuItemView
    {
        public List<TreeItem> TreeItems
        {
            get => _items;
            set
            {
                _history.Clear();
                _currentItems.Clear();
                _items = value;
                _currentItems.Add(1, _items[0]);
            }
        }

        private List<TreeItem> _items = new List<TreeItem>();

        private List<TreeItem> _history = new List<TreeItem>();

        private IDictionary<int, TreeItem> _currentItems = new Dictionary<int, TreeItem>();

        public void Display()
        {
            bool insideTree = true, correctOption = false;
            while (insideTree)
            {
                correctOption = false;
                DisplayElements();
                while (!correctOption)
                {
                    Console.WriteLine("Choose node to expand: \n0 to go back \n-1 to exit to menu ");
                    string choice = Console.ReadLine();
                    int number = int.Parse(choice);
                    if (_currentItems.ContainsKey(number) && _currentItems[number].Children != null)
                    {
                        _currentItems[number].IsExpanded = true;
                        UpdateCurrentItems(_currentItems[number], false);
                        correctOption = true;
                    }
                    else if (number == 0)
                    {
                        if (_history.Count > 1)
                        {
                            _history.RemoveAt(_history.Count - 1);
                        }

                        if (_history.Any())
                        {
                            TreeItem currentParent = _history.Last();
                            UpdateCurrentItems(currentParent, true);
                        }
                        else
                        {
                            _currentItems.Clear();
                            _currentItems.Add(1, _items[0]);
                        }

                        correctOption = true;
                    }
                    else if (number == -1)
                    {
                        correctOption = true;
                        insideTree = false;
                    }
                }
            }
        }

        public void UpdateCurrentItems(TreeItem currentParent, bool isBack)
        {
            int firstInt = 1;
            if (!isBack)
            {
                _history.Add(currentParent);
            }
            _currentItems = currentParent.Children.ToDictionary(x => (firstInt++), x => x);
        }

        public void DisplayElements()
        {
            foreach (KeyValuePair<int, TreeItem> currentItem in _currentItems)
            {
                if (currentItem.Value.Children != null)
                {
                    Console.WriteLine($"{currentItem.Key}:{currentItem.Value.TypeName} - {currentItem.Value.Name}");
                }
                else
                {
                    Console.WriteLine($"{currentItem.Value.TypeName} - {currentItem.Value.Name} -- write 'back'");
                }
            }
        }
    }
}
