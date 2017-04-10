using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Integrators
{
    public class TwitchIntegrator : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;
        TwitchClient _twitchClient;

        public TwitchIntegrator()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _twitchClient = new TwitchClient();
        }

        public TwitchIntegrator(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task AuthorizeTwitch(int userId, string authorizationCode)
        {//TODO chatch exceptions
            string token = await _twitchClient.GetAuthorizationToken(authorizationCode);

            if (token != null)
            {
                var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

                if (userIntegration != null)
                {
                    _unitOfWork.UserIntegrations.Delete(userIntegration);
                }

                var channel = await _twitchClient.GetChannel(token);

                //TODO make enum for inegration types
                _unitOfWork.UserIntegrations.Add(new UserIntegration
                {
                    UserID = userId,
                    IntegrationTypeID = 1,
                    Token = token,
                    URL = channel.url
                });

                _unitOfWork.Commit();
            }
            //TODO throw exception on invlid code?
        }

        public async Task DeAuthorizeTwitch(int userId)
        {
            var userInegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

            try
            {
                await _twitchClient.DeAuthorize(userInegration.Token);
                _unitOfWork.UserIntegrations.Delete(userInegration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsUserLive(int userId)
        {//TODO(Dave) refactor
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, 1);

            if (userIntegration != null)
            {
                string streamUrl = await _twitchClient.GetStreamUrlIfLive(userIntegration.Token);
                if (streamUrl != null)
                {
                    if (userIntegration.URL != streamUrl)
                    {
                        userIntegration.URL = streamUrl;
                        _unitOfWork.UserIntegrations.Update(userIntegration);
                        _unitOfWork.Commit();
                    }
                    return true;
                }
            }
            return false;
        }

        public string GetChannelUrl(int userId)
        {
            return _unitOfWork.UserIntegrations.GetById(userId, 1).URL;
        }
    }
}
