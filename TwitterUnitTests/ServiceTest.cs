using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using NUnit.Framework;

namespace TwitterUnitTests
{
    public class ServiceTest
    {
        [Test]
        public async Task TestGetDataSuccess()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {SetupTestData.BearerToken}");
            var resultData = await client.GetStreamAsync(SetupTestData.StreamUrl);
            Assert.NotNull(resultData);
        }

        [Test]
        public async Task TestGetDataFailure()
        {
            Exception exception = null;
            try
            {
                var client = new HttpClient();
                var invalidBearerToken = "invalidtoken";
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, invalidBearerToken);
                var resultData = await client.GetStreamAsync(SetupTestData.StreamUrl);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }
    }
}