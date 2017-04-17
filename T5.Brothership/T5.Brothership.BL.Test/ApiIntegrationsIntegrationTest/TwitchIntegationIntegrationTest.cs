using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Integrations;
using System.Threading.Tasks;
using System.Web;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;

namespace T5.Brothership.BL.Test.ApiIntegrationsIntegrationTest
{
    [TestClass]
    public class TwitchIntegationIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = DataContextCreator.CreateTestContext())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest"), ExpectedException(typeof(HttpException))]
        public async Task GetAuthorization_InvalidCodeIsNotInserted_IntegrationNotInDB()
        {
            using (var unitOfWork = new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()))
            {
                const string InValidCode = "ThisIsNotACode";
                const int expectedUserId = 2;

                using (TwitchIntegration twitchIntegration = new TwitchIntegration(unitOfWork, new TwitchClientFake()))
                {
                    await twitchIntegration.AuthorizeTwitch(expectedUserId, InValidCode);

                    var userIntegration = unitOfWork.UserIntegrations.GetById(expectedUserId, (int)IntegrationType.IntegrationTypes.Twitch);

                    Assert.IsNull(userIntegration);
                }
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task DeAuthorize_IsSucessfull_NoIntegrationInDB()
        {
            using (var unitOfWork = new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()))
            {
                const int expectedUserId = 2;
                unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitch,
                    Token = "token",
                    URL = "url",
                    UserID = expectedUserId,
                });
                unitOfWork.Commit();

                using (TwitchIntegration twitchIntegration = new TwitchIntegration(unitOfWork, new TwitchClientFake()))
                {
                    await twitchIntegration.DeAuthorizeTwitch(expectedUserId);

                    var userIntegration = unitOfWork.UserIntegrations.GetById(expectedUserId, (int)IntegrationType.IntegrationTypes.Twitch);

                    Assert.IsNull(userIntegration);
                }
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task IsUserLive_LiveUser_ReturnsTrue()
        {
            const int userId = 1;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = true
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()),
                                                                                twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsTrue(isUserLive);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task IsUserLive_NotLiveUser_ReturnsFalse()
        {
            const int userId = 1;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = false
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()),
                                                                                twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsFalse(isUserLive);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task IsUserLive_NoUserIntegration_ReturnsFalse()
        {
            const int userId = 2;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = false
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext()),
                                                           twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsFalse(isUserLive);
            }
        }
    }
}
