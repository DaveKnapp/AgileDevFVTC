using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using T5.Brothership.BL.TwitchApi;

namespace T5.Brothership.BL.Test
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

        public Task<bool> IsStreamerLive(string token)
        {
            return Task.Run(() => { return IsStreamLiveReturnValue; });
        }
    }
}
