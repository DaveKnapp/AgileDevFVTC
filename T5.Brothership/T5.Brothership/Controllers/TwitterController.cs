using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Integrations;

using Tweetinvi;
using Tweetinvi.Models;

namespace T5.Brothership.Controllers
{
    //TOOD(Dave) Test Controller
    public class TwitterController : Controller
    {
        private IAuthenticationContext _authenticationContext;
        private TwitterIntegration _twitterIntegration = new TwitterIntegration();

        public ActionResult AuthorizeTwitter()
        {
            var currentUser = Session["CurrentUser"] as T5.Brothership.Entities.Models.User;

            if (currentUser == null)
            {
                RedirectToAction("Login", "Login");
            }
            
            var appCreds = _twitterIntegration.GetCustomerCredentials();
            var redirectURL = "http://" + Request.Url.Authority + "/Twitter/ValidateTwitterAuth";
            _authenticationContext = AuthFlow.InitAuthentication(appCreds, redirectURL);

            return new RedirectResult(_authenticationContext.AuthorizationURL);
        }

        public ActionResult ValidateTwitterAuth()
        {
            var authorizationId = Request.Params.Get("authorization_id");
            var verifierCode = Request.Params.Get("oauth_verifier");

            var user = Session["CurrentUser"] as T5.Brothership.Entities.Models.User;

            _twitterIntegration.ValidateTwitterAuth(user.ID, authorizationId, verifierCode);

            return RedirectToAction("EditIntegrations", "Account");
        }

        public ActionResult DeAuthorize()
        {
            var user = Session["CurrentUser"] as T5.Brothership.Entities.Models.User;
            _twitterIntegration.DeAuthorize(user.ID);

            return RedirectToAction("EditIntegrations", "Account");
        }
    }
}