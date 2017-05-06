using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;

namespace T5.Brothership.BL.Test.ClientIntegrationTest
{
    [TestClass]
    public class TwitchClientIntegrationTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            using (var unitOfWork = new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()))
            using (var twitchClient = new TwitchClient())
            {
                var userIntegration = unitOfWork.UserIntegrations.GetById(1, (int)IntegrationType.IntegrationTypes.Twitch);

                var content = await twitchClient.GetRecentContent(userIntegration.ChannelId);
            }
        }
    }
}
