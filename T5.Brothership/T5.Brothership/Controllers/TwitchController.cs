using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;
using T5.Brothership.BL.Integrations;

namespace T5.Brothership.Controllers
{
    public class TwitchController : Controller
    {
        TwitchIntegration _twitchIntegration = new TwitchIntegration();

        //TODO(Dave) Menu and authorized are temporary things Implement UserIntegrations edit for account
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult AuthorizeTwitch()
        {
            StringBuilder urlBuilder = new StringBuilder("https://api.twitch.tv/kraken/oauth2/authorize");
            urlBuilder.Append("?response_type=code");
            urlBuilder.Append("&client_id=");
            urlBuilder.Append(T5.Brothership.BL.TwitchApi.ApiCredentials.CLIENT_ID);
            urlBuilder.Append("&redirect_uri=");
            urlBuilder.Append("http://" + Request.Url.Authority + "/Twitch/Authorize");
            urlBuilder.Append("&scope=user_read channel_read");
       
            return Redirect(urlBuilder.ToString());
        }

        public async Task<ActionResult> Authorize(string code, string scope)
        {
             User user = Session["CurrentUser"] as User;

            await _twitchIntegration.AuthorizeTwitch(user.ID, code);

            return RedirectToAction("EditIntegrations", "Account");
        }

        public ActionResult Authorized()
        {
            return View();
        }

        public async Task<ActionResult> DeAuthorize()
        {
            User user = Session["CurrentUser"] as User;
            await _twitchIntegration.DeAuthorizeTwitch(user.ID);

            return RedirectToAction("EditIntegrations", "Account");
        }
    }
}