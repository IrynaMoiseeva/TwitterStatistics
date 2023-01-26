using System.Text.Json.Serialization;

namespace TwitterStatistics
{
    public class Tweet
	{
		[JsonPropertyName("data")]
		public Data Data { get; set; }
	}

	public class Data
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }
	}
}