using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.BL.YoutubeApi
{
    public static class ApiCredentials
    {
        public const string CLIENT_ID = "269841690793-fo0a41pcc5vn4ink3gf3b4pnta1v90nq.apps.googleusercontent.com";
        public const string CLIENT_SECRET = "fTbzyCT_mWG83Qc1W6gkSKKW";
        public const string PROJECT_ID = "brothership-166001";
        public const string AUTH_URI = "https://accounts.google.com/o/oauth2/auth";
        public const string TOKEN_URI = "https://accounts.google.com/o/oauth2/token";
        public const string API_KEY = "AIzaSyBru9eZfHabtFl7T04z6esQ8lw1WWluTLc";
#if DEBUG
        public const string REDIRECT_URI = "http://localhost:60920/Youtube/AuthorizeYoutube";
#else
        public const string REDIRECT_URI = "http://brothership.azurewebsites.net/Youtube/AuthorizeYoutube";
#endif
    }
}
