using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;
using Tweetinvi.Models;

namespace T5.Brothership.BL.Test.IntegrationFakes
{
    public class TwitterIntegrationStub : ITwitterIntegration
    {
        public void DeAuthorize(int userId)
        {
        }

        public void Dispose()
        {
        }

        public ConsumerCredentials GetCustomerCredentials()
        {
            return new ConsumerCredentials(consumerKey: "consumerKey", consumerSecret: "consumerSecret");
        }

        public void Refresh(int userId)
        {
        }

        public void ValidateTwitterAuth(int userId, string authId, string verifierCode)
        {
        }
    }
}
