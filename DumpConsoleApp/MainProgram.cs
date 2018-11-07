using Autofac;
using Core;
using Core.Components;
using System;
using SharedUILogic;
using SharedUILogic.ViewModel;

namespace DumpConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            var container = ContainerProvider.BuildContainer("./");

            var source = container.Resolve<IStoreProvider>();

            var dict = source.GetAssemblyMetadataStore("./TPA.ApplicationArchitecture.dll");

            MainViewModel DataContext = Bootstraper.MainViewModel;

            MainView mV = new MainView(Bootstraper.MainViewModel);

            Console.ReadLine();
        }
    }
}
