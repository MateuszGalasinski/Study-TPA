using Autofac;
using Core;
using Core.Components;
using System;

namespace DumpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerProvider.BuildContainer("./");

            var source = container.Resolve<IDataSource>();

            Console.ReadLine();
        }
    }
}
