using Google.Apis.Auth.OAuth2.Responses;
using System.Threading.Tasks;

namespace T5.Brothership.BL.YoutubeApi
{
    public interface IYoutubeAuthClient
    {
        Task DeAuthorize(string token);
        void Dispose();
        Task<TokenResponse> GetAuthorizationToken(string authorizationCode);
    }
}