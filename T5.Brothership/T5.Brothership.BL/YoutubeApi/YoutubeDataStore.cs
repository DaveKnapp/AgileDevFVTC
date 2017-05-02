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
    public class YoutubeDataStore : IDataStore
    {
        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(string key)
        {
            return Task.Run(() => { });
        }

        public Task<T> GetAsync<T>(string key)
        {

            var taskSource = new TaskCompletionSource<T>();


            var user = new UserIntegration();


            using (BrothershipUnitOfWork unitOfWork = new BrothershipUnitOfWork())
            {
                user = unitOfWork.UserIntegrations.GetById(1, (int)IntegrationType.IntegrationTypes.Youtube);

                if (user == null)
                {
                    taskSource.SetResult(default(T));
                }
                else
                {
                    var tokenResponse = new TokenResponse
                    {
                        RefreshToken = user.RefreshToken,
                        AccessToken = user.Token
                    };
                    taskSource.SetResult((T)Convert.ChangeType(tokenResponse, typeof(T)));
                }

                return taskSource.Task;

            }

        }

        public Task StoreAsync<T>(string key, T value)
        {
            using (BrothershipUnitOfWork unitOfWork = new BrothershipUnitOfWork())
            {
                var taskSource = new TaskCompletionSource<T>();
                var tokenResponse = value as TokenResponse;
                var user = unitOfWork.UserIntegrations.GetById(1, (int)IntegrationType.IntegrationTypes.Youtube);

                if (user != null)
                {
                    user.RefreshToken = tokenResponse.RefreshToken;
                    unitOfWork.UserIntegrations.Update(user);
                    unitOfWork.Commit();
                }

                taskSource.SetResult((T)Convert.ChangeType(tokenResponse, typeof(T)));

                return taskSource.Task;

            }

        }
    }
}
