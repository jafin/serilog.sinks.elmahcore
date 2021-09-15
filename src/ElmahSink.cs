using ElmahCore;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.ElmahCore.Exceptions;

namespace Serilog.Sinks.ElmahCore
{
    public class ElmahSink : ILogEventSink
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ElmahSink()
        {
            //This can be used when configuring from config, as I'm unable to figure out how to allow appsettings config mapping with a sink constructor that has a injectable 
            //Dependency.
        }

        public ElmahSink(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level == LogEventLevel.Warning || logEvent.Level == LogEventLevel.Error ||
                logEvent.Level == LogEventLevel.Fatal)
            {
                //TODO: Anyway to set the message?
                ElmahExtensions.RiseError(logEvent.Exception ?? new WrappedElmahException(logEvent.RenderMessage()));

                //This mehtod is currently broken in ELMAHCore
                //_httpContextAccessor.HttpContext.RiseError(logEvent.Exception);
            }
        }
    }
}