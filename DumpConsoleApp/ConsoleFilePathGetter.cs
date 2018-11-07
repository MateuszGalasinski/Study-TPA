using System;
using SharedUILogic;

namespace DumpConsoleApp
{
    public class ConsoleFilePathGetter : IFilePathGetter
    {
        public string GetFilePath()
        {
            Console.WriteLine("Provide .dll file path: ");
            return Console.ReadLine();
        }
    }
}
