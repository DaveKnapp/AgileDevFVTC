﻿using System;
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
    public class TwitterIntegration : IDisposable, ITwitterIntegration
    {
        private IBrothershipUnitOfWork _unitOfWork;
        private ITwitterClient _twitterClient;

        public TwitterIntegration()
        {
            _twitterClient = new TwitterClient();
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public TwitterIntegration(ITwitterClient twitterClient, IBrothershipUnitOfWork unitOfWork)
        {
            _twitterClient = twitterClient;
            _unitOfWork = unitOfWork;
        }

        public Tweetinvi.Models.ConsumerCredentials GetCustomerCredentials()
        {
            return _twitterClient.GetCustomerCredentials();
        }

        public void ValidateTwitterAuth(int userId, string authId, string verifierCode)
        {
            var userCreds = _twitterClient.ValidateTwitterAuth(authId, verifierCode);

            SaveCredentials(userId, userCreds);
        }

        private void SaveCredentials(int userId, TwitterApiCredentials userCreds)
        {
            string userName = _twitterClient.GetUserName(userCreds.AccessToken, userCreds.AccessTokenSecret);
            DeleteUserIntegrationIfExists(userId);

            _unitOfWork.UserIntegrations.Add(new UserIntegration
            {
                IntegrationTypeID = (int)IntegrationType.IntegrationTypes.Twitter,
                UserID = userId,
                Token = userCreds.AccessToken,
                TokenSecret = userCreds.AccessTokenSecret,
                UserName = userName
            });
            _unitOfWork.Commit();
        }

        private void DeleteUserIntegrationIfExists(int userId)
        {
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

            if (userIntegration != null)
            {
                _unitOfWork.UserIntegrations.Delete(userIntegration);
            }
        }

        public void DeAuthorize(int userId)
        {
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);
            _unitOfWork.UserIntegrations.Delete(userIntegration);
            _unitOfWork.Commit();
        }

        public void Refresh(int userId)
        {
            var userIntegration = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitter);

            try
            {
                if (userIntegration != null)
                {
                    var twitterUrl = _twitterClient.GetUserName(userIntegration.Token, userIntegration.TokenSecret);
                    if (userIntegration.UserName != twitterUrl && twitterUrl != null)
                    {
                        userIntegration.UserName = twitterUrl;
                        _unitOfWork.UserIntegrations.Update(userIntegration);
                        _unitOfWork.Commit();
                    }
                }
            }
            catch (Exception)
            {
                //TOOD Handle exception
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
