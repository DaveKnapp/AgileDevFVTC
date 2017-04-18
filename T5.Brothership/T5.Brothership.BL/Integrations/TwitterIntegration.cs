using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Helpers;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;
using T5.Brothership.BL.TwitterApi;

namespace T5.Brothership.BL.Integrations
{
    public class TwitterIntegration
    {
        //TOOD Add integration Tests
        private IBrothershipUnitOfWork _brothershipUnitOfWork;
        private ITwitterClient _twitterClient;

        public TwitterIntegration()
        {
            _twitterClient = new TwitterClient();
            _brothershipUnitOfWork = new BrothershipUnitOfWork();
        }

        public TwitterIntegration(TwitterClient twitterClient, IBrothershipUnitOfWork unitOfWork)
        {
            _twitterClient = twitterClient;
            _brothershipUnitOfWork = unitOfWork;
        }

        public Tweetinvi.Models.ConsumerCredentials GetCustomerCredentials()
        {//TODO(Dave) Do I want to move this?
            return _twitterClient.GetCustomerCredentials();
        }

        public void ValidateTwitterAuth(int userId, string authId, string verifierCode)
        {
            var twitterClient = new TwitterClient();
            var userCreds = twitterClient.ValidateTwitterAuth(authId, verifierCode);

            SaveCredentials(userId, userCreds);
        }

        private void SaveCredentials(int userId, TwitterApiCredentials userCreds)
        {//TODO(Dave) Add check if userName Changed

            string userName = _twitterClient.GetUserURL(userCreds.AccessToken, userCreds.AccessTokenSecret);
            DeleteUserIntegrationIfExists(userId);

            _brothershipUnitOfWork.UserIntegrations.Add(new UserIntegration
            {
                IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitter,
                UserID = userId,
                Token = userCreds.AccessToken,
                TokenSecret = userCreds.AccessTokenSecret,
                URL = userName
            });
            _brothershipUnitOfWork.Commit();
        }

        private void DeleteUserIntegrationIfExists(int userId)
        {
            var userIntegration = _brothershipUnitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

            if (userIntegration != null)
            {
                _brothershipUnitOfWork.UserIntegrations.Delete(userIntegration);
            }
        }

        public void DeAuthorize(int userId)
        {
            var userIntegration = _brothershipUnitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);
            _brothershipUnitOfWork.UserIntegrations.Delete(userIntegration);
            _brothershipUnitOfWork.Commit();
        }

        public void Refresh(int userId)
        {
            var userIntegration = _brothershipUnitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

            try
            {
                var twitterUrl = _twitterClient.GetUserURL(userIntegration.Token, userIntegration.TokenSecret);
                if (userIntegration.URL != twitterUrl && twitterUrl != null)
                {
                    userIntegration.URL = twitterUrl;
                    _brothershipUnitOfWork.UserIntegrations.Update(userIntegration);
                    _brothershipUnitOfWork.Commit();
                }
            }
            catch (Exception)
            {
                //TOOD Handle exception
                throw;
            }
        }
    }
}
