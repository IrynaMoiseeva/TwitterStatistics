using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using NUnit.Framework;

namespace TwitterUnitTests
{
    public class ServiceTest
    {
        [Test]
        public async Task TestGetData()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {SetupTestData.BearerToken}");
            var resultData = await client.GetStreamAsync(SetupTestData.StreamUrl);
            Assert.NotNull(resultData);
        }
    }
}