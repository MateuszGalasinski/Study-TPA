using SharedUILogic;
using System;

namespace TreeViewConsoleApp
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
