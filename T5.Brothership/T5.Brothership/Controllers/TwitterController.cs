using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Integrations;
using T5.Brothership.Helpers;
using Tweetinvi;
using Tweetinvi.Models;

namespace T5.Brothership.Controllers
{
    public class TwitterController : Controller
    {//TODO Add tests Problem with request and AuthFlow need to mock?
        private IAuthenticationContext _authenticationContext;
        readonly ITwitterIntegration _twitterIntegration;
        readonly ISessionHelper _sessionHelper;

        public TwitterController() : this(new TwitterIntegration(), new SessionHelper()) { }

        public TwitterController(ITwitterIntegration twitterIntegration, ISessionHelper sessionHelper)
        {
            _twitterIntegration = twitterIntegration;
            _sessionHelper = sessionHelper;
        }

        public ActionResult AuthorizeTwitter()
        {
            var currentUser = _sessionHelper.Get("CurrentUser") as T5.Brothership.Entities.Models.User;

            if (currentUser == null)
            {
                RedirectToAction("Login", "Login");
            }
            
            var appCreds = _twitterIntegration.GetCustomerCredentials();
#if DEBUG
            var redirectURL = "http://localhost:60920/Twitter/ValidateTwitterAuth";
#else
            var redirectURL = "http://" + Request.Url.Authority + "/Twitter/ValidateTwitterAuth";
#endif
            _authenticationContext = AuthFlow.InitAuthentication(appCreds, redirectURL);

            return new RedirectResult(_authenticationContext.AuthorizationURL);
        }

        public ActionResult ValidateTwitterAuth()
        {
            var authorizationId = Request.Params.Get("authorization_id");
            var verifierCode = Request.Params.Get("oauth_verifier");

            var user = _sessionHelper.Get("CurrentUser") as T5.Brothership.Entities.Models.User;

            _twitterIntegration.ValidateTwitterAuth(user.ID, authorizationId, verifierCode);

            return RedirectToAction("EditIntegrations", "Account");
        }

        public ActionResult DeAuthorize()
        {
            var user = _sessionHelper.Get("CurrentUser") as T5.Brothership.Entities.Models.User;
            _twitterIntegration.DeAuthorize(user.ID);

            return RedirectToAction("EditIntegrations", "Account");
        }
    }
}