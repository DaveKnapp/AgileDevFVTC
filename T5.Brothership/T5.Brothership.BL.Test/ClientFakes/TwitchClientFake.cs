using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using T5.Brothership.BL.TwitchApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ClientFakes
{
    internal class TwitchClientFake : ITwitchClient
    {
        public bool IsStreamLiveReturnValue { get; set; }

        public async Task DeAuthorize(string token)
        {
            if (token == null)
            {
                throw new HttpException();
            }
            await Task.Run(() => { });
        }

        public Task<string> GetAuthorizationToken(string authorizationCode)
        {
            if (authorizationCode == "ThisIsACode")
            {
                return Task.Run(() => { return "ThisIsAToken"; });
            }
            else
            {
                throw new HttpException();
            }
        }

        public Task<Channel> GetChannel(string token)
        {
            return Task.Run(() =>
            {
                return new Channel
                {
                    mature = false,
                    status = "Dan is Batman - Telltale's Batman",
                    broadcaster_language = "en",
                    display_name = "DansGaming",
                    game = "BATMAN - The Telltale Series",
                    language = "en",
                    _id = 7236692,
                    name = "dansgaming",
                    created_at = new DateTime(2009, 07, 15),
                    updated_at = new DateTime(2016, 12, 15),
                    partner = true,
                    logo = "https=//static-cdn.jtvnw.net/jtv_user_pictures/dansgaming-profile_image-76e4a4ab9388bc9c-300x300.png",
                    video_banner = "https=//static-cdn.jtvnw.net/jtv_user_pictures/dansgaming-channel_offline_image-d3551503c24c08ad-1920x1080.png",
                    profile_banner = "https=//static-cdn.jtvnw.net/jtv_user_pictures/dansgaming-profile_banner-4c2b8ece8cd010b4-480.jpeg",
                    profile_banner_background_color = null,
                    url = "https=//www.twitch.tv/dansgaming",
                    views = 63906830,
                    followers = 538598

                };
            });
        }

        public Task<List<VideoContent>> GetRecentVideo(string channelId, int page, int itemsPerPage)
        {
            return Task.Run(() => { return CreateFakeVideos().Skip(page * itemsPerPage).Take(itemsPerPage).DefaultIfEmpty().ToList(); });
        }
    

        public Task<bool> IsStreamerLive(string token)
        {
            return Task.Run(() => { return IsStreamLiveReturnValue; });
        }

    private static List<VideoContent> CreateFakeVideos()
    {
        return new List<VideoContent>
                {
                    new VideoContent
                    {
                        Id = "54354235",
                        UploadTime = new DateTime(2017,5,7,10,10,43),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "234523454",
                        UploadTime = new DateTime(2017,5,7,1,40,14),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "345234542",
                        UploadTime = new DateTime(2017,5,6,10,30,00),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "4353532",
                        UploadTime = new DateTime(2017,5,5,8,30,00),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "7456745",
                        UploadTime = new DateTime(2017,5,3,15,0,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "4564565",
                        UploadTime = new DateTime(2017,5,2,7,30,00),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "574345654",
                        UploadTime = new DateTime(2017,5,2,7,30,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "3456546",
                        UploadTime = new DateTime(2017,5,2,18,30,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "2345534",
                        UploadTime = new DateTime(2017,5,1,7,0,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "5675675",
                        UploadTime = new DateTime(2017,4,29,20,05,43),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "2345435",
                        UploadTime = new DateTime(2017,4,28,7,12,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "32453453",
                        UploadTime = new DateTime(2017,4,27,10,00,23),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "67587658",
                        UploadTime = new DateTime(2017,4,26,14,45,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "5647567",
                        UploadTime = new DateTime(2017,4,23,10,00,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "56745746",
                        UploadTime = new DateTime(2017,4,21,18,0,0),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    },
                    new VideoContent
                    {
                        Id = "87676898",
                        UploadTime = new DateTime(2017,4,20,16,33,23),
                        ContentType = IntegrationType.IntegrationTypes.Twitch
                    }
                };
    }

}
}
