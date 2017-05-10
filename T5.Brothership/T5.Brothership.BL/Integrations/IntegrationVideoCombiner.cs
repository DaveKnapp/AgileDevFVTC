using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.BL.YoutubeApi;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Integrations
{
    public class IntegrationVideoCombiner : IDisposable
    {
        private readonly IYoutubeDataClient _youtubeDataClient;
        private readonly ITwitchClient _twitchClient;
        private readonly IBrothershipUnitOfWork _unitOfWork = new BrothershipUnitOfWork();

        public IntegrationVideoCombiner()
        {
            _youtubeDataClient = new YoutubeDataClient();
            _twitchClient = new TwitchClient();
        }

        public IntegrationVideoCombiner(IYoutubeDataClient youtubeDataClient, ITwitchClient twitchClient, IBrothershipUnitOfWork unitOfWork)
        {
            _youtubeDataClient = youtubeDataClient;
            _twitchClient = twitchClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<VideoContent>> GetRecentVideos(int userId, int qtyVideosToReturn, bool includeTwitch = true, bool includeYoutbe = true)
        {
            //TOOD(Dave) Refactor
            var twitchChannelId = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Twitch)?.ChannelId;
            var youtubeChannelId = _unitOfWork.UserIntegrations.GetById(userId, (int)IntegrationType.IntegrationTypes.Youtube)?.ChannelId;

            var youtubeVideoLoader = new YoutubeVideoLoader(_youtubeDataClient, youtubeChannelId);
            var twitchVideoLoader = new TwitchVideoLoader(_twitchClient, twitchChannelId);


            var videos = new List<VideoContent>();

            if (twitchChannelId == null)
            {
                twitchVideoLoader.HasMoreItems = false;
            }
            else
            {
                videos.AddRange(await twitchVideoLoader.GetVideos());
            }

            if (youtubeChannelId == null)
            {
                youtubeVideoLoader.HasMoreItems = false;
            }
            else
            {
                videos.AddRange(await youtubeVideoLoader.GetVideos());
            }

            videos = videos.OrderByDescending(p => p.UploadTime).ToList();

            var combinedVideos = new List<VideoContent>();

            while (videos.Count > 0)
            {
                var video = videos.First();
                combinedVideos.Add(video);
                videos.Remove(video);

                if (videos.Count(p => p.ContentType == IntegrationType.IntegrationTypes.Youtube) == 0 && youtubeVideoLoader.HasMoreItems)
                {
                    videos.AddRange(await youtubeVideoLoader.GetVideos());
                    videos = videos.OrderByDescending(p => p.UploadTime).ToList();
                }
                if (videos.Count(p => p.ContentType == IntegrationType.IntegrationTypes.Twitch) == 0 && twitchVideoLoader.HasMoreItems)
                {
                    videos.AddRange(await twitchVideoLoader.GetVideos());
                    videos = videos.OrderByDescending(p => p.UploadTime).ToList();
                }

                if (combinedVideos.Count >= qtyVideosToReturn || (videos.Count == 0))
                {
                    break;
                }
            }

            return combinedVideos;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
