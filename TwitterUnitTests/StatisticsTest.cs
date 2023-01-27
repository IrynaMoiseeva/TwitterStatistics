using System;
using System.Threading.Tasks;
using NUnit.Framework;
using TwitterStatistics;

namespace TwitterUnitTests
{
    public class StatisticsTest
    {
        [Test]
        public async Task TestUpdateStatistics()
        {
            var tweet = new Tweet
            {
                Data = new Data { Id = "123456", Text = "tweet1"}
            };
            var statistics = new Statistics();
            await Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => { statistics.UpdateStatistics(tweet); });
            Assert.AreEqual(statistics.TotalTweets, 1);
            Assert.AreEqual(statistics.TweetsWithUrlCount, 0);
        }

        [Test]
        public async Task TestUpdateStatisticsWithUrls()
        {
            var tweet = new Tweet
            {
                Data = new Data { Id = "123456", Text = "https://tweet1" }
            };
            var statistics = new Statistics();
            await Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => { statistics.UpdateStatistics(tweet); });
            Assert.AreEqual(statistics.TotalTweets, 1);
            Assert.AreEqual(statistics.TweetsWithUrlCount, 1);
        }
    }
}