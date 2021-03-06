﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Logic.Components;

namespace Logging
{
    [Export(typeof(ILogger))]
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
