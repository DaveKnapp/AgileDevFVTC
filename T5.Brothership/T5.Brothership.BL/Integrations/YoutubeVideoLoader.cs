using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.YoutubeApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Integrations
{
    public class YoutubeVideoLoader
    {
        public bool HasMoreItems { get; set; }

        private int _itemsPerPage = 5;
        private string _channelId;

        private readonly IYoutubeDataClient _YoutubeDataClient;

        public YoutubeVideoLoader(string channelId)
        {
            HasMoreItems = true;
            _YoutubeDataClient = new YoutubeDataClient();
            _channelId = channelId;
        }

        public YoutubeVideoLoader(IYoutubeDataClient youtubeDataClient, string channelId)
        {
            HasMoreItems = true;
            _YoutubeDataClient = youtubeDataClient;
            _channelId = channelId;
        }

        public async Task<List<VideoContent>> GetVideos()
        {
            if (HasMoreItems)
            {
                var videos = await _YoutubeDataClient.GetRecentVideos(_channelId, _itemsPerPage);

                if (videos.Count < _itemsPerPage)
                {
                    HasMoreItems = false;
                }

                return videos;
            }
            else
            {
                return new List<VideoContent>();
            }
        }
    }
}
