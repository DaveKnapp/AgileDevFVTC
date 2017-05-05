using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;

namespace T5.Brothership.BL.Test.IntegrationFakes
{
    public class YoutubeIntegrationStub : IYoutubeIntegration
    {
        public Task Authorize(int userId, string authorizationCode)
        {
            throw new NotImplementedException();
        }

        public Task DeAuthorize(int userId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<string> GetLiveStreamURLIfLive(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
