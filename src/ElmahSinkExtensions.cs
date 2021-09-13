using System;
using Serilog.Configuration;

namespace Serilog.Sinks.Elmah
{
    // ReSharper disable once UnusedType.Global
    public static class ElmahSinkExtensions
    {
        public static LoggerConfiguration ElmahSink(
            this LoggerSinkConfiguration loggerConfiguration,
            IFormatProvider fmtProvider = null)
        {
            return loggerConfiguration.Sink(new ElmahSink(fmtProvider));
        }
    }
}