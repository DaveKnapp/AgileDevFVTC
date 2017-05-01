using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.YoutubeApi;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Integrations
{
    public class YoutubeIntegration : IYoutubeIntegration
    {
        IBrothershipUnitOfWork _unitOfWork;
        IYoutubeAuthClient _youtubeAuthClient;

        public YoutubeIntegration()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _youtubeAuthClient = new YoutubeAuthClient();
        }

        public YoutubeIntegration(IBrothershipUnitOfWork unitOfWork, IYoutubeAuthClient twitchClient)
        {
            _unitOfWork = unitOfWork;
            _youtubeAuthClient = twitchClient;
        }

        public YoutubeIntegration(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task AuthorizeYoutube(int userId, string authorizationCode)
        {
            string token = await _youtubeAuthClient.GetAuthorizationToken(authorizationCode);

            if (token != null)
            {
                var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

                if (userIntegration != null)
                {
                    _unitOfWork.UserIntegrations.Delete(userIntegration);
                }

                // var channel = await _youtubeAuthClient.GetChannel(token);

                _unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Youtube,
                    Token = token,
                });
                _unitOfWork.Commit();

            }
            var dataClient = new YoutubeDataClient();
            string userName = await dataClient.GetUserName();
        }

        public async Task DeAuthorizeYoutube(int userId)
        {
            var userInegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

            try
            {
                await _youtubeAuthClient.DeAuthorize(userInegration.Token);
                _unitOfWork.UserIntegrations.Delete(userInegration);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
