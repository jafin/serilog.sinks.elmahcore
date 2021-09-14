using System;
using Microsoft.AspNetCore.Http;
using Serilog.Configuration;

namespace Serilog.Sinks.ElmahCore
{
    // ReSharper disable once UnusedType.Global
    public static class ElmahSinkExtensions
    {
        public static LoggerConfiguration ElmahCore(
            this LoggerSinkConfiguration loggerConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            return loggerConfiguration.Sink(new ElmahSink(httpContextAccessor));
        }

        public static LoggerConfiguration ElmahCore(
            this LoggerSinkConfiguration loggerConfiguration)
        {
            return loggerConfiguration.Sink(new ElmahSink());
        }
    }
}