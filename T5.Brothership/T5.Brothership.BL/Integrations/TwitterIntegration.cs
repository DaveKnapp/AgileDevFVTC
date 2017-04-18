using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Helpers;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;
using Tweetinvi;
using Tweetinvi.Models;

namespace T5.Brothership.BL.Integrations
{
    public class TwitterIntegration
    {
        //TOOD Add integration Tests
        private BrothershipUnitOfWork _brothershipUnitOfWork = new BrothershipUnitOfWork();

        public ConsumerCredentials GetCustomerCredentials()
        {//TODO(Dave) Do I want to move this?
            return new ConsumerCredentials("O0BhmFhwm6nFyRTqOaEcL7rnE", "qUj1OZiPkTqpfkFB32uedl6dWPgiNjIeuq8WJMPKKToOoIMPkc");
        }

        public void ValidateTwitterAuth(int userId, string authId, string verifierCode)
        {
            var userCreds = AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authId);
            Tweetinvi.Auth.Credentials = userCreds;

            SaveCredentials(userId, userCreds);
        }

        private void SaveCredentials(int userId, ITwitterCredentials userCreds)
        {//TODO(Dave) Add check if userName Changed
            var user = Tweetinvi.User.GetAuthenticatedUser(userCreds);

            var urlConverter = new TwitterURLConverter();
            var userName = urlConverter.GetURL(user.ScreenName);
            
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
    }
}
