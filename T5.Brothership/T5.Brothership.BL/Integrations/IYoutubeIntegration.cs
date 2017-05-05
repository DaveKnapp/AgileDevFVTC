using System.Threading.Tasks;

namespace T5.Brothership.BL.Integrations
{
    public interface IYoutubeIntegration
    {
        Task Authorize(int userId, string authorizationCode);
        Task DeAuthorize(int userId);
        Task<string> GetLiveStreamURLIfLive(int userId);
        void Dispose();
    }
}