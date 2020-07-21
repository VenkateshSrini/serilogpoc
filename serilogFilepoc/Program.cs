using System;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using serilogFilepoc.Service;

namespace serilogFilepoc
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var isConsole = (Debugger.IsAttached || args.Contains("--console"));
            var host = new HostBuilder()
                            .ConfigureAppConfiguration((hostingContext, config) =>
                            {
                                config.AddJsonFile("appsettings.json", optional: true);
                                config.AddEnvironmentVariables();
                                if (args != null)
                                {
                                    config.AddCommandLine(args);
                                }


                            })
                            .ConfigureLogging((hostingContext, loggingBuilder) =>
                            {
                                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                loggingBuilder.AddConsole();
                                Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(hostingContext.Configuration).CreateLogger();
                                loggingBuilder.AddSerilog(dispose: true);
                            })
                            .ConfigureServices((hostContext, services) =>
                            {
                                services.AddHostedService<SampleService>();
                            })
                            .Build();
            if (isConsole)
                await host.RunAsync();
            else
            {
                await host.StartAsync();
                await host.StopAsync();
                host.Dispose();
            }
        }
    }
}
