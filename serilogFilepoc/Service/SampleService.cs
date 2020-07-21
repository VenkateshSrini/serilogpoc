using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace serilogFilepoc.Service
{
    public class SampleService : BackgroundService
    {
        private readonly ILogger<BackgroundService> logger;
        public SampleService(ILogger<BackgroundService> logger)
        {
            this.logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Task invoked");
            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (File.Exists("Logs/log.txt"))
            {
                
                File.Copy("Logs/log.txt", $"Logs/log-{DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss")}.txt");
            }
            else
                logger.LogInformation("log file not found");

            return base.StopAsync(cancellationToken);
        }
    }
}
