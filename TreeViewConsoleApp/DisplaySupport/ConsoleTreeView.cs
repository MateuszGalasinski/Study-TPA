﻿using System;
using System.Collections.Generic;
using System.Linq;
using SharedUILogic.Model;

namespace TreeViewConsoleApp.DisplaySupport
{
    public class ConsoleTreeView
    {
        public List<TreeItem> TreeItems
        {
            get => _items;
            set
            {
                _history.Clear();
                _currentItems.Clear();
                _items = value;
                _currentItems.Add("A", _items[0]);
            }
        }

        private List<TreeItem> _items = new List<TreeItem>();

        private List<TreeItem> _history = new List<TreeItem>();

        private IDictionary<string, TreeItem> _currentItems = new Dictionary<string, TreeItem>();

        public void Display()
        {
            bool insideTree = true, correctOption = false;
            while (insideTree)
            {
                correctOption = false;
                DisplayElements();
                while (!correctOption)
                {
                    Console.WriteLine("Choose node to expand: ");
                    string choice = Console.ReadLine();
                    if (_currentItems.ContainsKey(choice) && _currentItems[choice].IsExpendable)
                    {
                        UpdateCurrentItems(_currentItems[choice], false);
                        correctOption = true;
                    }
                    else if (choice == "back")
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
                            _currentItems.Add("A", _items[0]);
                        }

                        correctOption = true;
                    }
                    else if (choice == "quit")
                    {
                        correctOption = true;
                        insideTree = false;
                    }
                }
            }
        }

        public void UpdateCurrentItems(TreeItem currentParent, bool isBack)
        {
            char firstChar = (char)65;
            if (!isBack)
            {
                _history.Add(currentParent);
            }

            _currentItems = currentParent.Children.ToDictionary(x => (firstChar++).ToString(), x => x);
        }

        public void DisplayElements()
        {
            foreach (KeyValuePair<string, TreeItem> currentItem in _currentItems)
            {
                if (currentItem.Value.IsExpendable == false)
                {
                    Console.WriteLine($"{currentItem.Key} - {currentItem.Value.Name} -- not expendable");
                }
                else
                {
                    Console.WriteLine($"{currentItem.Key} - {currentItem.Value.Name}");
                }
            }
        }
    }
}