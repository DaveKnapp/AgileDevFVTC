using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.YoutubeApi;
using System.Threading.Tasks;

namespace T5.Brothership.BL.Test.ApiIntegrationsIntegrationTest
{
    [TestClass]
    public class YoutubeIntegrationIntegrationTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            YoutubeDataClient client = new YoutubeDataClient();

            string streamId = await client.GetLiveStreamIdIfLive(1);

        }
    }
}
