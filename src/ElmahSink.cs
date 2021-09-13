using System;
using Serilog.Core;
using Serilog.Events;
using Elmah;

namespace Serilog.Sinks.Elmah
{
    public class ElmahSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public ElmahSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level == LogEventLevel.Warning || logEvent.Level == LogEventLevel.Error ||
                logEvent.Level == LogEventLevel.Fatal)
            {
                ErrorLog.GetDefault(null).Log(new Error(logEvent.Exception)
                {
                    Message = $"{logEvent.RenderMessage(_formatProvider)} {logEvent.Exception?.Message}"
                });
            }
        }
    }
}