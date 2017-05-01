using System.Threading.Tasks;

namespace T5.Brothership.BL.Integrations
{
    public interface IYoutubeIntegration
    {
        Task AuthorizeYoutube(int userId, string authorizationCode);
        Task DeAuthorizeYoutube(int userId);
        void Dispose();
    }
}