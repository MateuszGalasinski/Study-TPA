﻿using Logging;
using System;
using UILogic.ViewModel;
using XmlSerialization;

namespace TreeViewConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            MainView mV = new MainView(new MainViewModel(new ConsoleFilePathGetter(), new Logger(), new XmlDataContractSerializer()));

            mV.List();

            Console.ReadLine();
        }
    }
}
