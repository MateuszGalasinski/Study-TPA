using System;
using SharedUILogic;

namespace TreeViewConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            MainView mV = new MainView(Bootstraper.MainViewModel);

            mV.List();

            Console.ReadLine();
        }
    }
}
