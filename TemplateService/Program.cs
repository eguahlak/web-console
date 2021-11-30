using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TemplateService
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(configureLogging => configureLogging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(;
                    services.AddHostedService<ImageClassifierWorker>()
                        .Configure<EventLogSettings>(config =>
                        {
                            config.LogName = "Image Classifier Service";
                            config.SourceName = "Image Classifier Service Source";
                        });
                }).UseWindowsService();
    }
}
