using ReflectionLoading;
using System;
using UILogic.ViewModel;

namespace TreeViewConsoleApp
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            MainView mainView = new MainView(new MainViewModel(new ConsoleFilePathGetter(), new AssemblyManager()));

            mainView.List();

            Console.ReadLine();
        }
    }
}
