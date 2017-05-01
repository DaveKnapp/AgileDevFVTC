using Google.Apis.Auth.OAuth2.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using System.Threading;
using System.Net.Http;

namespace T5.Brothership.BL.YoutubeApi
{
    public class AppCodeReciever : ICodeReceiver
    {
        public AppCodeReciever()
        {

        }

        public AppCodeReciever(string code)
        {
            Code = code;
        }

        public string Code { get; set; }

        public string RedirectUri
        {
            get
            {
                return ApiCredentials.REDIRECT_URI;
            }
        }

        public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(AuthorizationCodeRequestUrl url, CancellationToken taskCancellationToken)
        {
            YoutubeAuthClient client = new YoutubeAuthClient();


            var response = new AuthorizationCodeResponseUrl
            {
                Code = await client.RefreshToken(Code)
            };

            return response;
        }
    }
}
