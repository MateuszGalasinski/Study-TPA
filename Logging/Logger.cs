using System;
using System.Diagnostics;
using Core.Components;

namespace Logging
{
    public class Logger : ILogger
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(DateTime.Now + " | " + message);
        }

        public void Info(string message)
        {
            Debug.WriteLine(DateTime.Now + " | " + message);
        }
    }
}
