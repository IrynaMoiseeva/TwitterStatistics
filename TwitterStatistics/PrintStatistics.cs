using System;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TwitterStatistics
{
    public class PrintStatistics : IPrintStatistics
	{
		private readonly IStatistics _statistics;
		private readonly IConfiguration _configuration;
		private readonly ILogger<IPrintStatistics> _logger;

		private Timer _timer;

		public PrintStatistics(IStatistics statistics,
			                   IConfiguration configuration,
							   ILogger<IPrintStatistics> logger)
        {
			_statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_timer = new Timer();
			_timer.Elapsed += Timer_Elapsed;
		}

		public void Start()
        {
			var twitterStatisticsConfig = _configuration.GetSection("TwitterStatisticsConfig");
			var intervalInSeconds = twitterStatisticsConfig["IntervalInSeconds"];
			if (int.TryParse(intervalInSeconds, out int seconds))
			{
				_timer.Interval = seconds * 1000;
			}
			else
            {
				_timer.Interval = 1000;
				_logger.LogError("Error occuring while parsing configuration");
			}

			_timer.Start();
		}

		public void Stop()
        {
			_timer.Stop();
			_timer.Dispose();
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			PrintResult();
		}

		private void PrintResult()
		{
			Console.Clear();
			Console.WriteLine("Press any key to exit");
			Console.WriteLine("***=============***");
			Console.WriteLine($"Total number of tweets received: {_statistics.TotalTweets}");
			Console.WriteLine($"Average tweets per minute: {_statistics.TweetsPerMinute}");
			Console.WriteLine($"Total number of tweets with Http: {_statistics.TweetsWithUrlCount}");
		}
	}
}

