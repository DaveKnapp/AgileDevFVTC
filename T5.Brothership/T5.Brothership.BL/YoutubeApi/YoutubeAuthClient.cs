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
using Google.Apis.Auth.OAuth2.Responses;

namespace T5.Brothership.BL.YoutubeApi
{
    public class YoutubeAuthClient : IYoutubeAuthClient
    {
        private readonly HttpClient _client;
        public YoutubeAuthClient()
        {
            _client = new HttpClient();
            InitializeClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<TokenResponse> GetAuthorizationToken(string authorizationCode)
        {
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.CLIENT_SECRET);
            values.Add("grant_type", "authorization_code");
            values.Add("redirect_uri", ApiCredentials.REDIRECT_URI);
            values.Add("code", authorizationCode);

            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync("oauth2/v4/token", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var authorizationResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonString);

                return new TokenResponse
                {
                    AccessToken = authorizationResponse.access_token,
                    RefreshToken = authorizationResponse.refresh_token,
                    ExpiresInSeconds = authorizationResponse.expires_in
                };
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task<string> RefreshToken(string token)
        {
            var values = new Dictionary<string, string>();

            values.Add("client_id", ApiCredentials.CLIENT_ID);
            values.Add("client_secret", ApiCredentials.CLIENT_SECRET);
            values.Add("refresh_token", token);
            values.Add("grant_type", "refresh_token");

            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync("oauth2/v4/token", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var authorizationResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonString);

                return authorizationResponse.refresh_token == null ? authorizationResponse.access_token : authorizationResponse.refresh_token;
            }
            else
            {
                throw new HttpException(response.StatusCode.ToString());
            }
        }

        public async Task DeAuthorize(string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://accounts.google.com/");

                var response = await client.GetAsync("o/oauth2/revoke?token=" + token);


                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    throw new HttpException(response.StatusCode.ToString());
                }
            }
        }

        private void InitializeClient()
        {
            _client.BaseAddress = new Uri("https://www.googleapis.com/");
        }
    }
}
