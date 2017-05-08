using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.TwitchApi
{
    public interface ITwitchClient
    {
        Task<string> GetAuthorizationToken(string authorizationCode);
        Task<Channel> GetChannel(string token);
        Task<bool> IsStreamerLive(string token);
        Task DeAuthorize(string token);
        Task<List<VideoContent>> GetRecentVideo(string channelId, int page, int itemsPerPage);
    }
}