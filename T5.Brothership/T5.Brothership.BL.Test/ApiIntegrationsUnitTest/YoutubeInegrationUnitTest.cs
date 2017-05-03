using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Integrations;
using T5.Brothership.PL.Test;
using T5.Brothership.BL.Test.ClientFakes;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using System.Web;

namespace T5.Brothership.BL.Test.ApiIntegrationsUnitTest
{
    [TestClass]
    public class YoutubeInegrationUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public async Task Authorize_SuccessfullAuthorizationIntegrationAdded_UserIntegrationNotNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const int userId = 1;
                const string validCode = "ValidCode";

                using (var youtubeInegration = new YoutubeIntegration(unitOfWork,
                                                                        new YoutubeAuthClientFake(validCode),
                                                                        new YoutubeDataClientFake()))
                {
                    await youtubeInegration.Authorize(userId, validCode);
                }

                var userInegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);
                Assert.IsNotNull(userInegration);
            }
        }

        [TestMethod, TestCategory("UnitTest"), ExpectedException(typeof(HttpException))]
        public async Task Authorize_UnSuccessfullThrowsException_ExceptionEqualHTTPException()
        {
            const int userId = 1;
            const string validCode = "ValidCode";

            using (var youtubeInegration = new YoutubeIntegration(new FakeBrothershipUnitOfWork(),
                                                                    new YoutubeAuthClientFake(validCode),
                                                                    new YoutubeDataClientFake()))
            {
                await youtubeInegration.Authorize(userId, "InvalidCode");
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task Authorize_UnSuccessfullAuthorizationIntegrationNotAdded_UserIntegrationIsNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const int userId = 1;
                const string validCode = "ValidCode";

                using (var youtubeInegration = new YoutubeIntegration(unitOfWork,
                                                                        new YoutubeAuthClientFake("InvalidCode"),
                                                                        new YoutubeDataClientFake()))
                {
                    try
                    {
                        await youtubeInegration.Authorize(userId, validCode);
                    }
                    catch (Exception)
                    {
                        var userInegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);
                        Assert.IsNull(userInegration);
                        return;
                    }
                }
            }
            Assert.Fail();
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task DeAuthorize_SuccessfullDeAuthorizationIntegrationDeleted_UserIntegrationIsNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const int userId = 1;
                const string validCode = "ValidCode";

                unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    ChannelId = "ChannelId",
                    RefreshToken = "ValidCode",
                    Token = "Token",
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Youtube,
                });
                unitOfWork.Commit();


                using (var youtubeInegration = new YoutubeIntegration(unitOfWork,
                                                                        new YoutubeAuthClientFake(validCode),
                                                                        new YoutubeDataClientFake()))
                {
                    await youtubeInegration.DeAuthorize(userId);
                }

                var userInegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);
                Assert.IsNull(userInegration);
            }
        }

        [TestMethod, TestCategory("UnitTest"), ExpectedException(typeof(HttpException))]
        public async Task DeAuthorize_UnSuccessfullAuthorizationThrowException_ExceptionEqualHttpException()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const int userId = 1;
                const string validCode = "ValidCode";

                unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    ChannelId = "ChannelId",
                    RefreshToken = "InvalidCode",
                    Token = "Token",
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Youtube,
                });
                unitOfWork.Commit();


                using (var youtubeInegration = new YoutubeIntegration(unitOfWork,
                                                                        new YoutubeAuthClientFake(validCode),
                                                                        new YoutubeDataClientFake()))
                {
                    await youtubeInegration.DeAuthorize(userId);
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task DeAuthorize_UnSuccessfullAuthorizationIntegrationNotDeleted_IntegrationNotNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                const int userId = 1;
                const string validCode = "ValidCode";

                unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    ChannelId = "ChannelId",
                    RefreshToken = "InvalidCode",
                    Token = "Token",
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Youtube,
                });
                unitOfWork.Commit();


                using (var youtubeInegration = new YoutubeIntegration(unitOfWork,
                                                                        new YoutubeAuthClientFake(validCode),
                                                                        new YoutubeDataClientFake()))
                {
                    try
                    {
                        await youtubeInegration.DeAuthorize(userId);
                    }
                    catch (Exception)
                    {
                        var userIntegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);
                        Assert.IsNotNull(userIntegration);
                        return;
                    }
                }
            }
            Assert.Fail();
        }
    }
}
