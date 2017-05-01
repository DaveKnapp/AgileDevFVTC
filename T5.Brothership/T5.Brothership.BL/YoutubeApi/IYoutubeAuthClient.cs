using System.Threading.Tasks;

namespace T5.Brothership.BL.YoutubeApi
{
    public interface IYoutubeAuthClient
    {
        Task DeAuthorize(string token);
        void Dispose();
        Task<string> GetAuthorizationToken(string authorizationCode);
    }
}