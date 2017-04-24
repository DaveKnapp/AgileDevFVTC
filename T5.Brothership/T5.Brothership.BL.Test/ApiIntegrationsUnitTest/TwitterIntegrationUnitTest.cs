using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Integrations;
using T5.Brothership.BL.Test.ClientFakes;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ApiIntegrationUnitTest
{
    [TestClass]
    public class TwitterIntegrationUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void ValidateTwitterAuth_WasIntegrationInsertedOnValidCode_DbIntegrationNotNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (TwitterIntegration twitterIntegration = new TwitterIntegration(new TwitterClientFake(), unitOfWork))
                {
                    const int userId = 2;
                    twitterIntegration.ValidateTwitterAuth(userId, TwitterClientFake.VALID_AUTH_ID, TwitterClientFake.VALID_VERIFIER_CODE);

                    var actualUserIntegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

                    Assert.IsNotNull(actualUserIntegration);
                }
            }
        }
        [TestMethod, TestCategory("UnitTest")]
        public void DeAuthorize_WasIntegrationDeleted_IntegrationIsNull()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (TwitterIntegration twitterIntegration = new TwitterIntegration(new TwitterClientFake(), unitOfWork))
                {
                    const int userId = 2;
                    unitOfWork.UserIntegrations.Add(new UserIntegration
                    {
                        UserID = userId,
                        UserName = "www.testurl.com",
                        Token = "TestToken",
                        TokenSecret = "TestTokenSecret",
                        IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitter
                    });
                    unitOfWork.Commit();

                    twitterIntegration.DeAuthorize(userId);
                    var actualUserIntegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

                    Assert.IsNull(actualUserIntegration);
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Refresh_TwitterUrlChangedIfDifferent_URLUpdated()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (TwitterIntegration twitterIntegration = new TwitterIntegration(new TwitterClientFake(), unitOfWork))
                {
                    const int userId = 2;
                    unitOfWork.UserIntegrations.Add(new UserIntegration
                    {
                        UserID = userId,
                        UserName = "www.testurl.com",
                        Token = TwitterClientFake.VALID_ACCESS_TOKEN,
                        TokenSecret = TwitterClientFake.VALID_ACCESS_TOKEN_SECRET,
                        IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitter
                    });
                    unitOfWork.Commit();

                    twitterIntegration.Refresh(userId);
                    var actualUserIntegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

                    Assert.AreEqual(actualUserIntegration.UserName, TwitterClientFake.URL);
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Refresh_TwitterUrlNotChangedIfSame_URLUpdated()
        {
            using (var unitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (TwitterIntegration twitterIntegration = new TwitterIntegration(new TwitterClientFake(), unitOfWork))
                {
                    const int userId = 2;
                    unitOfWork.UserIntegrations.Add(new UserIntegration
                    {
                        UserID = userId,
                        UserName = TwitterClientFake.URL,
                        Token = TwitterClientFake.VALID_ACCESS_TOKEN,
                        TokenSecret = TwitterClientFake.VALID_ACCESS_TOKEN_SECRET,
                        IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitter
                    });
                    unitOfWork.Commit();

                    twitterIntegration.Refresh(userId);
                    var actualUserIntegration = unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

                    Assert.AreEqual(actualUserIntegration.UserName, TwitterClientFake.URL);
                }
            }
        }
    }
}
