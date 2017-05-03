using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T5.Brothership.PL;

namespace T5.Brothership.BL.YoutubeApi
{
    public class YoutubeDataClient : IYoutubeDataClient
    {
        private IBrothershipUnitOfWork _unitOfWork;
        private readonly ClientSecrets secrets = new ClientSecrets
        {
            ClientId = ApiCredentials.CLIENT_ID,
            ClientSecret = ApiCredentials.CLIENT_SECRET
        };

        public YoutubeDataClient()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public async Task<string> GetChannelId(int userId)
        {
            var dataStore = new YoutubeDataStore();

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                  new[] { YouTubeService.Scope.YoutubeReadonly },
                 userId.ToString(),
                 CancellationToken.None,
                 new YoutubeDataStore()
               );

            using (var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Brothership"
            }))
            {
                var channelsListRequest = youtubeService.Channels.List("contentDetails");
                channelsListRequest.Mine = true;

                var channelsListResponse = await channelsListRequest.ExecuteAsync();

                return channelsListResponse.Items[0].Id;
            }
        }

    }
}
