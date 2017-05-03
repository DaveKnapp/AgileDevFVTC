using Google.Apis.Auth.OAuth2.Responses;
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
    {//TOOD(Dave) Add test
        IBrothershipUnitOfWork _unitOfWork;
        IYoutubeAuthClient _youtubeAuthClient;
        YoutubeDataClient _youtubeDataClient;

        public YoutubeIntegration()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _youtubeAuthClient = new YoutubeAuthClient();
            _youtubeDataClient = new YoutubeDataClient();
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

        public async Task Authorize(int userId, string authorizationCode)
        {
            TokenResponse tokenResponse = await _youtubeAuthClient.GetAuthorizationToken(authorizationCode);

            if (tokenResponse != null)
            {
                var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

                if (userIntegration != null)
                {
                    _unitOfWork.UserIntegrations.Delete(userIntegration);
                    _unitOfWork.Commit();
                }

                _unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Youtube,
                    RefreshToken = tokenResponse.RefreshToken,
                    Token = tokenResponse.AccessToken
                });
                _unitOfWork.Commit();

                await SetChannelId(userId);
            }

        }

        public async Task DeAuthorize(int userId)
        {
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

            if (userIntegration != null)
            {
                try
                {
                    await _youtubeAuthClient.DeAuthorize(userIntegration.RefreshToken);
                    _unitOfWork.UserIntegrations.Delete(userIntegration);
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private async Task SetChannelId(int userId)
        {
            string channelId = await _youtubeDataClient.GetChannelId(userId);

            if (channelId != null)
            {
                var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);
                userIntegration.ChannelId = channelId;

                _unitOfWork.UserIntegrations.Update(userIntegration);
                _unitOfWork.Commit();
            }
        }
    }
}

