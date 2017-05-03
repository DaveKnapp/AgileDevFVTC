using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using Newtonsoft.Json;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.YoutubeApi
{
    public class YoutubeDataStore : IDataStore, IDisposable
    {
        private IBrothershipUnitOfWork _unitOfWork;

        public YoutubeDataStore()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(string key)
        {
            int userId = Convert.ToInt32(key);
            return Task.Run(() =>
            {
                var user = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

                if (user != null)
                {
                    user.RefreshToken = null;
                    _unitOfWork.UserIntegrations.Update(user);
                    _unitOfWork.Commit();
                }
            });
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        public Task<T> GetAsync<T>(string key)
        {
            int userId = Convert.ToInt32(key);
            var taskSource = new TaskCompletionSource<T>();

            var user = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

            if (user == null)
            {
                taskSource.SetResult(default(T));
            }
            else
            {
                var tokenResponse = new TokenResponse
                {
                    RefreshToken = user.RefreshToken,
                };
                taskSource.SetResult((T)Convert.ChangeType(tokenResponse, typeof(T)));
            }

            return taskSource.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            int userId = Convert.ToInt32(key);
            var taskSource = new TaskCompletionSource<T>();
            var tokenResponse = value as TokenResponse;

            var user = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube);

            if (user != null)
            {
                user.Token = tokenResponse.AccessToken;
                user.RefreshToken = tokenResponse.RefreshToken;
                _unitOfWork.UserIntegrations.Update(user);
                _unitOfWork.Commit();
            }

            taskSource.SetResult((T)Convert.ChangeType(tokenResponse, typeof(T)));

            return taskSource.Task;
        }
    }
}
