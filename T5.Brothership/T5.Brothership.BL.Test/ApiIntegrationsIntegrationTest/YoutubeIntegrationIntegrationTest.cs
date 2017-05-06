using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.YoutubeApi;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ApiIntegrationsIntegrationTest
{
    [TestClass]
    public class YoutubeIntegrationIntegrationTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            YoutubeDataClient client = new YoutubeDataClient();

            var unitOfWork = new BrothershipUnitOfWork();
            var channelId = unitOfWork.UserIntegrations.GetById(1, (int)IntegrationType.IntegrationTypes.Youtube).ChannelId;

            var content = await client.GetRecentVideos(channelId);

        }
    }
}
