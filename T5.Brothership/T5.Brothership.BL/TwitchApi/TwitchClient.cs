using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using T5.Brothership.BL.TwitchApi;

namespace T5.Brothership.BL.TwitchApi
{
    public class TwitchClient : IDisposable, ITwitchClient
    {
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
                //TODO throw custom exeption?
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task<bool> IsStreamerLive(string token)
        {//TODO(dave) create nullURl object?
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
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }

            return false;
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

        private void CreateClientHeaders()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            client.DefaultRequestHeaders.Add("Client-ID", ApiCredentials.CLIENT_ID);
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/");
        }
    }
}
