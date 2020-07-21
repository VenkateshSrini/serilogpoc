using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
