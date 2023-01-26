using System.Diagnostics;

namespace TwitterStatistics
{
    public class Statistics: IStatistics
	{
		private const int SECONDS_IN_ONE_MINUTE = 60;
		private Stopwatch _stopwatch;

		public Statistics()
		{
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
		}

		public int TotalTweets { get; set; }
		public int TweetsWithUrlCount { get; set; }
		public int TweetsPerMinute { get; set; }

		public void UpdateStatistics(Tweet tweet)
		{
			TotalTweets++;
			TweetsPerMinute = TotalTweets / (int)_stopwatch.Elapsed.TotalSeconds * SECONDS_IN_ONE_MINUTE;
			CalculateTweetsWithUrl(tweet);
		}

		private void CalculateTweetsWithUrl(Tweet tweet)
		{
			if (tweet.Data.Text.Contains("http"))
			{
				TweetsWithUrlCount++;
			}
		}
	}
}