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
            MainView mV = new MainView(Bootstraper.MainViewModel);

            mV.List();

            Console.ReadLine();
        }
    }
}
