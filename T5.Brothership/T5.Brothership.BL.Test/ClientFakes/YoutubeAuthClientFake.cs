using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using T5.Brothership.BL.YoutubeApi;

namespace T5.Brothership.BL.Test.ClientFakes
{
    public class YoutubeAuthClientFake : IYoutubeAuthClient
    {
        public string ValidCode { get; set; }

        public YoutubeAuthClientFake()
        {

        }

        public YoutubeAuthClientFake(string validCode)
        {
            ValidCode = validCode;
        }

        public Task DeAuthorize(string token)
        {
            if (token == ValidCode)
            {
                return Task.Run(() => { });
            }
            else
            {
                throw new HttpException();
            }
        }

        public void Dispose()
        {
        }

        public Task<TokenResponse> GetAuthorizationToken(string authorizationCode)
        {
            if (authorizationCode == ValidCode)
            {
                return Task.Run(() =>
                {
                    return new TokenResponse
                    {
                        AccessToken = "AccessToken",
                        RefreshToken = "RefreshToke",
                    };
                });
            }
            else
            {
                throw new HttpException();
            }
        }
    }
}
