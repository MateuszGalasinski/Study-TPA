﻿namespace Core.Components
{
    public interface ILogger
    {
        void Trace(string message);

        void Info(string message);
    }
}