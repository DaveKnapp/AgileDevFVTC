using Google.Apis.Services;
using Google.Apis.YouTube;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace T5.Brothership.Controllers
{
    public class YoutubeController : Controller
    {
        public ActionResult Authorize()
        {
            var builder = new StringBuilder();
            builder.Append(@"https://accounts.google.com/o/oauth2/v2/auth?");
            builder.Append(@"scope=https://www.googleapis.com/auth/youtube.readonly&");
            builder.Append(@"access_type=offline&");
            builder.Append(@"include_granted_scopes=true&");
            builder.Append(@"redirect_uri=http://localhost:60920/Youtube/AuthorizeYoutube&");
            //state = state_parameter_passthrough_value &
            builder.Append(@"response_type=code &");
            builder.Append(@"client_id=269841690793-fo0a41pcc5vn4ink3gf3b4pnta1v90nq.apps.googleusercontent.com");
                                
            return Redirect(builder.ToString());
        }

        public ActionResult AuthorizeYoutube(string code)
        {
            return View();
        }
    }
}