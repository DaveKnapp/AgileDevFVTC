using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.BL.Helpers;
using Tweetinvi;
using Tweetinvi.Models;

namespace T5.Brothership.BL.TwitterApi
{
    public class TwitterClient
    {

        public ITwitterCredentials ValidateTwitterAuth(string authId, string verifierCode)
        {
            return AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authId);
        }

        public string GetUserURL(TwitterCredentials twitterCredentials)
        {
            Tweetinvi.Auth.Credentials = twitterCredentials;
            var user = Tweetinvi.User.GetAuthenticatedUser(twitterCredentials);

            var urlConverter = new TwitterURLConverter();
            return urlConverter.GetURL(user.ScreenName);
        }
    }
}