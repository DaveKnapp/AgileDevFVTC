using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.BL.Helpers;
using Tweetinvi;
using Tweetinvi.Models;

namespace T5.Brothership.BL.TwitterApi
{
    public class TwitterClient : ITwitterClient
    {
        public ConsumerCredentials GetCustomerCredentials()
        {
            return new ConsumerCredentials("O0BhmFhwm6nFyRTqOaEcL7rnE", "qUj1OZiPkTqpfkFB32uedl6dWPgiNjIeuq8WJMPKKToOoIMPkc");
        }

        public TwitterApiCredentials ValidateTwitterAuth(string authId, string verifierCode)
        {
            var credentials = AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authId);

            return new TwitterApiCredentials
            {
                AccessToken = credentials.AccessToken,
                AccessTokenSecret = credentials.AccessTokenSecret
            };
        }

        public string GetUserName(string accessToken, string accessSecret)
        {
            Tweetinvi.Auth.Credentials = new TwitterCredentials
            {
                AccessToken = accessToken,
                AccessTokenSecret = accessSecret,
                ConsumerKey = GetCustomerCredentials().ConsumerKey,
                ConsumerSecret = GetCustomerCredentials().ConsumerSecret
            };

            var user = Tweetinvi.User.GetAuthenticatedUser();

            return user.ScreenName;
        }
    }
}