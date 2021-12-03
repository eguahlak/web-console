using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// New-Service -Name {name} -BinaryPathName {Path} -Description {Desc} -DisplayName {Disp} -StartupType Automatic 

namespace TemplateWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly ExampleInstrument instrument;
        private readonly IHostApplicationLifetime lifetime;

        public Worker(IHostApplicationLifetime lifetime, ILogger<Worker> logger)
        {
            this.logger = logger;
            this.instrument = new ExampleInstrument();
            this.lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await instrument.Run(stoppingToken);
            lifetime.StopApplication();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
