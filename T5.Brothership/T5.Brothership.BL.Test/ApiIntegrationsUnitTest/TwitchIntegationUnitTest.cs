using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using System.Web;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.BL.Test.ClientFakes;

namespace T5.Brothership.BL.Test.ApiIntegrationsUnitTest
{
    [TestClass]
    public class TwitchIntegationUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAuthorization_SuccessfullAthorization_IntegrationIsInserted()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const string validCode = "ThisIsACode";
                const int expectedUserId = 2;
                const string expectedToken = "ThisIsAToken";
                const string expectedUrl = @"https=//www.twitch.tv/dansgaming";

                using (TwitchIntegration twitchIntegration = new TwitchIntegration(unitOfWork, new TwitchClientFake()))
                {
                    await twitchIntegration.AuthorizeTwitch(expectedUserId, validCode);

                    var userIntegration = unitOfWork.UserIntegrations.GetById(expectedUserId, (int)IntegrationType.IntegrationTypes.Twitch);

                    Assert.AreEqual((int)IntegrationType.IntegrationTypes.Twitch, userIntegration.IntegrationTypeID);
                    Assert.AreEqual(expectedUserId, userIntegration.UserID);
                    Assert.AreEqual(expectedToken, userIntegration.Token);
                    Assert.AreEqual(expectedUrl, userIntegration.URL);
                }
            }

        }

        [TestMethod, TestCategory("UnitTest"), ExpectedException(typeof(HttpException))]
        public async Task GetAuthorization_InvalidCodeFails_ThrowsHTTPException()
        {
            const string InValidCode = "ThisIsNotACode";
            const int userId = 1;
            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new FakeBrothershipUnitOfWork(), new TwitchClientFake()))
            {
                await twitchIntegration.AuthorizeTwitch(userId, InValidCode);
            }
        }

        [TestMethod, TestCategory("UnitTest"), ExpectedException(typeof(HttpException))]
        public async Task GetAuthorization_InvalidCodeIsNotInserted_IntegrationNotInDB()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
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

        [TestMethod, TestCategory("UnitTest")]
        public async Task DeAuthorize_IsSucessfull_NoIntegrationInDB()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
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

        [TestMethod, TestCategory("UnitTest")]
        public async Task IsUserLive_LiveUser_ReturnsTrue()
        {
            const int userId = 1;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = true
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new FakeBrothershipUnitOfWork(), twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsTrue(isUserLive);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task IsUserLive_NotLiveUser_ReturnsFalse()
        {
            const int userId = 1;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = false
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new FakeBrothershipUnitOfWork(), twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsFalse(isUserLive);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task IsUserLive_NoUserIntegration_ReturnsFalse()
        {
            const int userId = 2;

            var twitchClient = new TwitchClientFake
            {
                IsStreamLiveReturnValue = false
            };

            using (TwitchIntegration twitchIntegration = new TwitchIntegration(new FakeBrothershipUnitOfWork(), twitchClient))
            {
                bool isUserLive = await twitchIntegration.IsUserLive(userId);
                Assert.IsFalse(isUserLive);
            }
        }
    }
}
