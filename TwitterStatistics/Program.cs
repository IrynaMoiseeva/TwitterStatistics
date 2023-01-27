using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace TwitterStatistics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(log =>
                {
                    log.SetBasePath(Directory.GetCurrentDirectory());
                    log.AddJsonFile("appsettings.json", false, true);
                })
                .ConfigureLogging((configuration, log) =>
                {
                    log.AddConfiguration(configuration.Configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // here, we register the dependency injection
                    services.AddSingleton<ITwitterService, TwitterService>();
                    services.AddSingleton<IStatistics, Statistics>();
                    services.AddSingleton<IPrintStatistics, PrintStatistics>();

                }).UseConsoleLifetime();

            var host = builder.Build();

            var statisticsService = host.Services.GetRequiredService<ITwitterService>();
            var printStatistics = host.Services.GetRequiredService<IPrintStatistics>();
            _ = statisticsService.ReadDataAsync();

            printStatistics.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            printStatistics.Stop();
        }
    }
}

