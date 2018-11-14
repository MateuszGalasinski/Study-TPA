using System;
using SharedUILogic;
using UILogic;

namespace TreeViewConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            MainView mV = GetMainViewModel();
            mV.List();
            Console.ReadLine();
        }
        private MainView GetMainViewModel()
        {
            return new MainViewModel(new ConsoleFilePathGetter(), new Logger());
        }
    }
}
