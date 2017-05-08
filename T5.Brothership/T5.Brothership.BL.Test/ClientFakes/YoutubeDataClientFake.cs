using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.YoutubeApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ClientFakes
{
    public class YoutubeDataClientFake : IYoutubeDataClient
    {
        private int page = 0;

        public Task<string> GetChannelId(int userId)
        {
            return Task.Run(() => "ChannelId");
        }

        public Task<string> GetLiveStreamIdIfLive(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VideoContent>> GetRecentVideos(string channelId, int itemsPerPage)
        {
            return Task.Run(() => {
                var videos = createFakeVideos().Skip(page * itemsPerPage).Take(itemsPerPage).DefaultIfEmpty().ToList();
                page++;
                return videos;
            });

        }

        private List<VideoContent> createFakeVideos()
        {
            return
                    new List<VideoContent>
                    {
                    new VideoContent
                    {
                        Id = "24352345",
                        UploadTime = new DateTime(2017,5,7,7,30,2),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "456456",
                        UploadTime = new DateTime(2017,5,6,8,40,0),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "345645",
                        UploadTime = new DateTime(2017,5,5,12,30,30),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "3546536",
                        UploadTime = new DateTime(2017,5,4,8,00,00),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "567657",
                        UploadTime = new DateTime(2017,5,3,5,45,0),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "456346",
                        UploadTime = new DateTime(2017,5,2,7,00,00),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "3546546",
                        UploadTime = new DateTime(2017,5,2,15,10,0),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "24354235",
                        UploadTime = new DateTime(2017,5,1,6,00,45),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "7654764",
                        UploadTime = new DateTime(2017,4,30,7,0,0),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "657456",
                        UploadTime = new DateTime(2017,4,29,4,35,15),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "654764",
                        UploadTime = new DateTime(2017,4,28,5,30,0),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "65474",
                        UploadTime = new DateTime(2017,4,27,12,11,53),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    },
                    new VideoContent
                    {
                        Id = "45354665",
                        UploadTime = new DateTime(2017,4,23,6,33,44),
                        ContentType = IntegrationType.IntegrationTypes.Youtube
                    }
                    };
        }
    }
}
