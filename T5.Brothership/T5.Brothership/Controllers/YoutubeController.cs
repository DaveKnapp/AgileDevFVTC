using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;
using T5.Brothership.Helpers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.Controllers
{//TOOD Test
    public class YoutubeController : Controller
    {
        IYoutubeIntegration _youtubeIntegration;
        ISessionHelper _sessionHelper;

        public YoutubeController() : this(new YoutubeIntegration(), new SessionHelper()) { }

        public YoutubeController(IYoutubeIntegration youtubeIntegration, ISessionHelper sessionHelper)
        {
            _youtubeIntegration = youtubeIntegration;
            _sessionHelper = sessionHelper;
        }


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

        public async Task<ActionResult> AuthorizeYoutube(string code)
        {
            var user = _sessionHelper.Get("CurrentUser") as User;

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            await _youtubeIntegration.Authorize(user.ID, code);

            return RedirectToAction("EditIntegrations", "Account");
        }

        public async Task<ActionResult> DeAuthorize()
        {
            var user = _sessionHelper.Get("CurrentUser") as User;

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            await _youtubeIntegration.DeAuthorize(user.ID);

            return RedirectToAction("EditIntegrations", "Account");
        }
    }
}