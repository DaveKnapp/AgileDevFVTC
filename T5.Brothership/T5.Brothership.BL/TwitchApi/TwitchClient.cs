using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.TwitchApi
{
    public class TwitchClient : ITwitchClient
    {//TOOD(Dave) Handle null responses
        private readonly HttpClient client = new HttpClient();

        public TwitchClient()
        {
            CreateClientHeaders();
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<string> GetAuthorizationToken(string authorizationCode)
        {
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.SECRET);
            values.Add("grant_type", "authorization_code");
            values.Add("redirect_uri", ApiCredentials.REDIRECT_URL);
            values.Add("code", authorizationCode);

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("oauth2/token", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(jsonString);
                return authorizationResponse.access_token;
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }
        public async Task DeAuthorize(string token)
        {
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.SECRET);
            values.Add("token", token);

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("oauth2/revoke", content);

            if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task<bool> IsStreamerLive(string token)
        {
            var channel = await GetChannel(token);

            var response = await client.GetAsync("streams/" + channel._id + "/?stream_type=live");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var streamResponse = JsonConvert.DeserializeObject<StreamResponse>(jsonString);
                if (streamResponse.stream != null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task<Channel> GetChannel(string token)
        {
            client.DefaultRequestHeaders.Add("Authorization", "OAuth " + token);
            var response = await client.GetAsync("channel");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var channel = JsonConvert.DeserializeObject<Channel>(jsonString);
                client.DefaultRequestHeaders.Remove("Authorization");
                return channel;
            }
            else
            {
                throw new HttpRequestException(response.StatusCode.ToString());
            }
        }

        public async Task<List<VideoContent>> GetRecentVideo(string channelId, int page, int itemsPerPage)
        {
            if (itemsPerPage > 100)
            {
                throw new ArgumentException("items per page must be less than or equal to 1000");
            }

            var response = await client.GetAsync("channels/" + channelId + "/videos/?limit=" + itemsPerPage + "&offset=" + page * itemsPerPage);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                var videoResponse = JsonConvert.DeserializeObject<ChannelVideoResponse>(jsonString);

                var content = new List<VideoContent>();

                foreach (var stream in videoResponse.videos)
                {
                    content.Add(new VideoContent
                    {
                        Id = stream._id.ToString(),
                        UploadTime = stream.published_at,
                        ContentType = IntegrationType.IntegrationTypes.Twitch,
                        PreviewImgUrl = stream.preview.medium,
                        Title = stream.title
                      
                    });
                }

                return content;

            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
            throw new NotImplementedException();
        }


        private void CreateClientHeaders()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            client.DefaultRequestHeaders.Add("Client-ID", ApiCredentials.CLIENT_ID);
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/");
        }
    }
}
