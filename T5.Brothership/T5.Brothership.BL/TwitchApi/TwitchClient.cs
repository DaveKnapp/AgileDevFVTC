using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.BL.TwitchApi
{
    public class TwitchClient
    {
        private readonly HttpClient client = new HttpClient();

        public TwitchClient()
        {
            client = CreateClient();
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }
        public void Authorize(int itemsPerPage, int currentPage)
        {

            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/games/top/");

        }

        private HttpClient CreateClient()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/streams/");

            return client;
        }

        private string CreateRequestString(int itemsPerPage, int currentPage)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"?client_id=" + ApiCredentials.CLIENT_ID);
            builder.Append("&limit=" + itemsPerPage);
            builder.Append("&offset=" + currentPage * itemsPerPage);

            return builder.ToString();
        }
    }
}
