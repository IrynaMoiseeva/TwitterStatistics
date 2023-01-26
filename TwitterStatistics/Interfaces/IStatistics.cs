namespace TwitterStatistics
{
    public interface IStatistics
    {
        public int TotalTweets { get; set; }
        public int TweetsWithUrlCount { get; set; }
        public int TweetsPerMinute { get; set; }

        public void UpdateStatistics(Tweet tweet);
    }
}