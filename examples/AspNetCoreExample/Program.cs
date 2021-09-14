using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.ElmahCore;

namespace AspNetCoreExample
{
    public class Program
    {
        public static ConfigStyle ConfigStyle = ConfigStyle.LoadFromAppSettings;

        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                {
                    switch (ConfigStyle)
                    {
                        case ConfigStyle.LoadFromAppSettings:
                            //TODO: Figure out how to accept dependency injection via constructor when using ReadFrom.Config
                            configuration.ReadFrom.Configuration(context.Configuration)
                                .Enrich.FromLogContext();
                            break;
                        case ConfigStyle.InlineConfig:
                            configuration.WriteTo.ElmahCore(services.GetService<IHttpContextAccessor>())
                                .Enrich.FromLogContext()
                                .WriteTo.Console();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}