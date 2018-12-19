using System;
using UILogic.Services;
using UILogic.ViewModel;

namespace TreeViewConsoleApp
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            MainViewModel mainViewModel = Composer.GetComposedMainViewModel(new ConsoleFilePathGetter());

            MainView mainView = new MainView(mainViewModel);

            mainView.List();

            Console.ReadLine();
        }
    }
}
