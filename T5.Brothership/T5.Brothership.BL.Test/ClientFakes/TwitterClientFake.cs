using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.TwitterApi;
using Tweetinvi.Models;

namespace T5.Brothership.BL.Test.ClientFakes
{
    public class TwitterClientFake : ITwitterClient
    {
        public const string VALID_ACCESS_TOKEN = "ValidAccessToken";
        public const string VALID_ACCESS_TOKEN_SECRET = "ValidAccessTokenSecret";
        public const string VALID_AUTH_ID = "ValidAccessToken";
        public const string VALID_VERIFIER_CODE = "ValidAccessTokenSecret";
        public const string URL = "https://wwww.twitter.com/Test";
        
        public Tweetinvi.Models.ConsumerCredentials GetCustomerCredentials()
        {
            return new ConsumerCredentials("O0BhmFhwm6nFyRTqOaEcL7rnE", "qUj1OZiPkTqpfkFB32uedl6dWPgiNjIeuq8WJMPKKToOoIMPkc");
        }

        public string GetUserURL(string accessToken, string accessSecret)
        {
            if (accessToken == VALID_ACCESS_TOKEN || accessSecret == VALID_ACCESS_TOKEN_SECRET)
            {
                return URL;
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public TwitterApiCredentials ValidateTwitterAuth(string authId, string verifierCode)
        {
            if (authId == VALID_AUTH_ID || verifierCode == VALID_VERIFIER_CODE)
            {
                return new TwitterApiCredentials
                {
                    AccessToken = VALID_ACCESS_TOKEN,
                    AccessTokenSecret = VALID_ACCESS_TOKEN_SECRET
                };
            }
            else
            {
                throw new ApplicationException();
            }
        }
    }
}
