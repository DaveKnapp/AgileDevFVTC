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

namespace T5.Brothership.BL.YoutubeApi
{
    public class YoutubeDataClient
    {
        public async Task<string> GetChannelId()
        {
            var secrets = new ClientSecrets
            {
                ClientId = ApiCredentials.CLIENT_ID,
                ClientSecret = ApiCredentials.CLIENT_SECRET
            };


            var dataStore = new YoutubeDataStore();
            var user = await dataStore.GetAsync<TokenResponse>("test");
          
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                  new[] { YouTubeService.Scope.YoutubeReadonly },
                 "user",
                 CancellationToken.None,
                 new YoutubeDataStore(),
                 new AppCodeReciever(user.RefreshToken)
               );


            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Brothership"
            });

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            var channelsListResponse = await channelsListRequest.ExecuteAsync();

            return channelsListResponse.Items[0].Id;
        }
    }
}
