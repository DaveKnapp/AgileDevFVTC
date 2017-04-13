using System.Threading.Tasks;

namespace T5.Brothership.BL.TwitchApi
{
    public interface ITwitchClient
    {
        Task<string> GetAuthorizationToken(string authorizationCode);
        Task<Channel> GetChannel(string token);
        Task<bool> IsStreamerLive(string token);
        Task DeAuthorize(string token);
    }
}