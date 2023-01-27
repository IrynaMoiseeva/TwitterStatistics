using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace TwitterStatistics
{
    public class TwitterService : ITwitterService
    {
        private readonly ILogger<TwitterService> _logger;
        private readonly IStatistics _statistics;
        private readonly IConfiguration _configuration;

        public TwitterService(ILogger<TwitterService> logger,
                                     IStatistics statistics,
                                     IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Fetch data from Twitter
        /// </summary>
        public async Task ReadDataAsync()
        {
            try
            {
                var twitterStatisticsConfig = _configuration.GetSection("TwitterStatisticsConfig");
                var bearerToken = twitterStatisticsConfig["BearerToken"];
                var streamUrl = twitterStatisticsConfig["StreamUrl"];

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {bearerToken}");
                using (var stream = await httpClient.GetStreamAsync(streamUrl))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = await reader.ReadLineAsync();
                                _logger.LogTrace(line);
                                var tweet = new Tweet();
                                if (TryDeserializeTweet(line, out tweet))
                                {
                                    _statistics.UpdateStatistics(tweet);
                                }
                            }
                        }
                    }
                }
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occuring while getting data from Twitter");
                throw ex;
            }
        }

        private bool TryDeserializeTweet(string data, out Tweet tweet)
        {
            tweet = new Tweet();

            try
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    throw new ArgumentException("Object cannot be parsed");
                }

                tweet = JsonSerializer.Deserialize<Tweet>(data);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during deserialization");
                return false;
            }
        }
    }
}