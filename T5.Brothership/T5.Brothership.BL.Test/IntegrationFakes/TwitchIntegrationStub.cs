using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;

namespace T5.Brothership.BL.Test.IntegrationFakes
{
    public class TwitchIntegrationStub : ITwitchIntegration
    {
        public bool IsUserLiveReturn { get; set; }
        public string GetChannelUrlReturn { get; set; }

        public Task AuthorizeTwitch(int userId, string authorizationCode)
        {
            return Task.Run(() => { });
        }

        public Task DeAuthorizeTwitch(int userId)
        {
            return Task.Run(() => { });
        }

        public void Dispose()
        {
        }

        public string GetChannelUrl(int userId)
        {
            return GetChannelUrlReturn;
        }

        public Task<bool> IsUserLive(int userId)
        {
            return Task.Run(() => { return IsUserLiveReturn; });
        }
    }
}
