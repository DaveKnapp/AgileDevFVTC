using System.Collections.Generic;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.YoutubeApi
{
    public interface IYoutubeDataClient
    {
        Task<string> GetChannelId(int userId);
        Task<string> GetLiveStreamIdIfLive(int userId);
        Task<List<VideoContent>> GetRecentVideos(string channelId, int itemsPerPage);
    }
}