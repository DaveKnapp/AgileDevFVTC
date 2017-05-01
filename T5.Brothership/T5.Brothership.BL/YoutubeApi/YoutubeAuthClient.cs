using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;

namespace T5.Brothership.BL.YoutubeApi
{
    public class YoutubeAuthClient : IYoutubeAuthClient
    {
        private readonly HttpClient client;
        public YoutubeAuthClient()
        {
            client = new HttpClient();
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
            values.Add("client_secret", ApiCredentials.CLIENT_SECRET);
            values.Add("grant_type", "authorization_code");
            values.Add("redirect_uri", ApiCredentials.REDIRECT_URI);
            values.Add("code", authorizationCode);

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("oauth2/v4/token", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var authorizationResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonString);
                //TODO(Dave) Currently this is returning a refresh token but storing in db as token.  Do we want to add refreshed token column to DB?
                return authorizationResponse.refresh_token != null ? authorizationResponse.refresh_token : null;
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task<string> RefreshToken(string token)
        {
            //refresh_token =< refresh_token > &
            //grant_type = refresh_token
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.CLIENT_SECRET);
            values.Add("refresh_token", token);
            values.Add("grant_type", "refresh_token");

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("oauth2/v4/token", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var authorizationResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonString);
                //TODO(Dave) Currently this is returning a refresh token but storing in db as token.  Do we want to add refreshed token column to DB?
                return authorizationResponse.refresh_token == null ? authorizationResponse.access_token : authorizationResponse.refresh_token;
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task DeAuthorize(string token)
        {
            throw new NotImplementedException();
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.CLIENT_SECRET);
            values.Add("token", token);

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("oauth2/revoke", content);

            if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        private void CreateClientHeaders()
        {
            client.BaseAddress = new Uri("https://www.googleapis.com/");
        }
    }
}
