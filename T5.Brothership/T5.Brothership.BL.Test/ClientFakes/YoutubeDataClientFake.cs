using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.YoutubeApi;

namespace T5.Brothership.BL.Test.ClientFakes
{
    public class YoutubeDataClientFake : IYoutubeDataClient
    {
        public Task<string> GetChannelId(int userId)
        {
            return Task.Run(() => "ChannelId");
        }

        public Task<string> GetLiveStreamIdIfLive(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
