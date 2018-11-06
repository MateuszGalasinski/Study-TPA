using Autofac;
using Core;
using Core.Components;
using System;

namespace DumpConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = ContainerProvider.BuildContainer("./");

            var source = container.Resolve<IStoreProvider>();

            var dict = source.GetAssemblyMetadataStore("./TPA.ApplicationArchitecture.dll");

            Console.ReadLine();
        }
    }
}
