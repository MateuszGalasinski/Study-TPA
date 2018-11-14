using System;
using UILogic.ViewModel;
using Logging;

namespace TreeViewConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            MainView mV = new MainView(new MainViewModel(new ConsoleFilePathGetter(), new Logger()));

            mV.List();

            Console.ReadLine();
        }
    }
}
