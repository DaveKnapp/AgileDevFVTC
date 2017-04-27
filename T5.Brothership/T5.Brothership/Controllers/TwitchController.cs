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
using T5.Brothership.Helpers;

namespace T5.Brothership.Controllers
{
    public class TwitchController : Controller
    {
        ITwitchIntegration _twitchIntegration;
        ISessionHelper _sessionHelper;

        public TwitchController() : this(new TwitchIntegration(), new SessionHelper()) { }

        public TwitchController(ITwitchIntegration twitchIntegration, ISessionHelper sessionHelper)
        {
            _twitchIntegration = twitchIntegration;
            _sessionHelper = sessionHelper;
        }

        public ActionResult AuthorizeTwitch()
        {
            StringBuilder urlBuilder = new StringBuilder("https://api.twitch.tv/kraken/oauth2/authorize");
            urlBuilder.Append("?response_type=code");
            urlBuilder.Append("&client_id=");
            urlBuilder.Append(T5.Brothership.BL.TwitchApi.ApiCredentials.CLIENT_ID);
            urlBuilder.Append("&redirect_uri=");
            urlBuilder.Append(T5.Brothership.BL.TwitchApi.ApiCredentials.REDIRECT_URL);
            urlBuilder.Append("&scope=user_read channel_read");
       
            return Redirect(urlBuilder.ToString());
        }

        public async Task<ActionResult> Authorize(string code, string scope)
        {
             User user = _sessionHelper.Get("CurrentUser") as User;

            await _twitchIntegration.AuthorizeTwitch(user.ID, code);

            return RedirectToAction("EditIntegrations", "Account");
        }

        public async Task<ActionResult> DeAuthorize()
        {
            User user = _sessionHelper.Get("CurrentUser") as User;
            await _twitchIntegration.DeAuthorizeTwitch(user.ID);

            return RedirectToAction("EditIntegrations", "Account");
        }
    }
}