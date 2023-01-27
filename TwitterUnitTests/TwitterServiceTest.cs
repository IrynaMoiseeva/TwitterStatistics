using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using TwitterStatistics;
using Microsoft.Extensions.Logging;

namespace TwitterUnitTests
{
    public class TwitterServiceTest
    {
        [Test]
        public async Task TestReadDataWithException()
        {
            Exception exception = null;
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("invalidappsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());
            try
            {
                ILogger<TwitterService> logger = loggerFactory.CreateLogger<TwitterService>();
                var target = new TwitterService(logger, new Statistics(), configuration);
                await target.ReadDataAsync();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }
    }
}