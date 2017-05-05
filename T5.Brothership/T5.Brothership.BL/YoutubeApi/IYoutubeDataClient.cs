using System.Threading.Tasks;

namespace T5.Brothership.BL.YoutubeApi
{
    public interface IYoutubeDataClient
    {
        Task<string> GetChannelId(int userId);
        Task<string> GetLiveStreamIdIfLive(int userId);
    }
}