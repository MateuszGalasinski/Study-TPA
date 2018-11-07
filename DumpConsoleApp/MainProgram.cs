using Autofac;
using Core;
using Core.Components;
using System;
using SharedUILogic;

namespace DumpConsoleApp
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            var container = ContainerProvider.BuildContainer("./");

            var source = container.Resolve<IStoreProvider>();

            var dict = source.GetAssemblyMetadataStore("./TPA.ApplicationArchitecture.dll");

            MainView mV = new MainView(Bootstraper.MainViewModel);

            mV.Display();

            Console.ReadLine();
        }
    }
}
