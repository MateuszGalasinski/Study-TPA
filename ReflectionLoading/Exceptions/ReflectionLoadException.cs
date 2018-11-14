using System;

namespace ReflectionLoading.Exceptions
{
    public class ReflectionLoadException : ApplicationException
    {
        public ReflectionLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
