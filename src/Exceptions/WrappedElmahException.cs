using System;

namespace Serilog.Sinks.ElmahCore.Exceptions
{
    public class WrappedElmahException : Exception
    {
        public WrappedElmahException(string message) : base(message)
        {
        }

        public WrappedElmahException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}