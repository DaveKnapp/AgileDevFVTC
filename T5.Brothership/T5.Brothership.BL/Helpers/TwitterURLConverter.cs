using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.BL.Helpers
{
    public class TwitterURLConverter
    {
        //https://twitter.com/BrotherShipDev
        private const string BEGINNING_URL = "https://twitter.com/";

        public string GetUserName(string url)
        {
            StringBuilder builder = new StringBuilder(url);
            builder.Replace(BEGINNING_URL, "");
            return builder.ToString();
        }

        public string GetURL(string username)
        {
            return BEGINNING_URL + username;
        }
    }
}
