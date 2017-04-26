using System.Threading.Tasks;

namespace T5.Brothership.BL.Integrations
{
    public interface ITwitchIntegration
    {
        Task AuthorizeTwitch(int userId, string authorizationCode);
        Task DeAuthorizeTwitch(int userId);
        void Dispose();
        string GetChannelUrl(int userId);
        Task<bool> IsUserLive(int userId);
    }
}