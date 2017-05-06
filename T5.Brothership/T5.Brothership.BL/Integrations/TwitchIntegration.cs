using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Integrations
{
    public class TwitchIntegration : IDisposable, ITwitchIntegration
    {
        IBrothershipUnitOfWork _unitOfWork;
        ITwitchClient _twitchClient;

        public TwitchIntegration()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _twitchClient = new TwitchClient();
        }

        public TwitchIntegration(IBrothershipUnitOfWork unitOfWork, ITwitchClient twitchClient)
        {
            _unitOfWork = unitOfWork;
            _twitchClient = twitchClient;
        }

        public TwitchIntegration(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task AuthorizeTwitch(int userId, string authorizationCode)
        {
            string token = await _twitchClient.GetAuthorizationToken(authorizationCode);

            if (token != null)
            {
                var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

                if (userIntegration != null)
                {
                    _unitOfWork.UserIntegrations.Delete(userIntegration);
                }

                var channel = await _twitchClient.GetChannel(token);

                _unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitch,
                    Token = token,
                    UserName = channel.name,
                    ChannelId = channel._id.ToString()
                });
                _unitOfWork.Commit();
            }
        }

        public async Task DeAuthorizeTwitch(int userId)
        {
            var userInegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

            try
            {
                await _twitchClient.DeAuthorize(userInegration.Token);
                _unitOfWork.UserIntegrations.Delete(userInegration);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsUserLive(int userId)
        {
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

            if (userIntegration != null && await _twitchClient.IsStreamerLive(userIntegration.Token))
            {
                await UpdateStreamUrl(userIntegration);

                return true;
            }

            return false;
        }

        private async Task UpdateStreamUrl(UserIntegration userIntegration)
        {
            var streamChannel = await _twitchClient.GetChannel(userIntegration.Token);

            if (streamChannel != null && userIntegration.UserName != streamChannel.name)
            {
                userIntegration.UserName = streamChannel.name;
                _unitOfWork.UserIntegrations.Update(userIntegration);
                _unitOfWork.Commit();
            }
        }

        public string GetChannelUrl(int userId)
        {
            var userName = _unitOfWork.UserIntegrations.GetById(userId, 1).UserName;
            return "https://www.twitch.tv/" + userName;
        }
    }
}
