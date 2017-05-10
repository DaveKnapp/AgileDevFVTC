using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Integrations
{
    public class TwitchVideoLoader : ITwitchVideoLoader
    {
        public bool HasMoreItems { get; set; } 

        private int _currentPage = 0;
        //TODO Change to 100
        private int _itemsPerPage = 10;
        private string _channelId;

        private readonly ITwitchClient _twitchClient;


        public TwitchVideoLoader(string channelId)
        {
            HasMoreItems = true;
            _twitchClient = new TwitchClient();
            _channelId = channelId;
        }

        public TwitchVideoLoader(ITwitchClient twitchClient, string channelId)
        {
            HasMoreItems = true;
            _twitchClient = twitchClient;
            _channelId = channelId;
        }

        public async Task<List<VideoContent>> GetVideos()
        {
            if (HasMoreItems)
            {
                var videos = await _twitchClient.GetRecentVideo(_channelId, _currentPage, _itemsPerPage);

                if (videos.Count < _itemsPerPage)
                {
                    HasMoreItems = false;
                }

                _currentPage++;

                return videos;
            }
            else
            {
                return new List<VideoContent>();
            }
        }
    }
}
