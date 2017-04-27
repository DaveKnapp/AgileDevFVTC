using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Controllers;
using T5.Brothership.BL.Test.IntegrationFakes;
using System.Web.Mvc;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;

namespace T5.Brothership.Web.Test.ControllersTest
{
    [TestClass]
    public class TwitchControllerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void AuthorizeTwitch_RedirectToCorrectUrl_UrlContainsTwitchUrl()
        {
            using (var controller = new TwitchController(new TwitchIntegrationStub(), new SessionHelperFake()))
            {
                var results = controller.AuthorizeTwitch() as RedirectResult;

                string expectedUrl = "https://api.twitch.tv/kraken/oauth2/authorize";

                Assert.IsTrue(results.Url.Contains(expectedUrl));
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task Authorize_SuccessfullAuthorizationReturnsCorrectAction_ExpectedActionEqualsActual()
        {
            var session = new SessionHelperFake();
            var user = new User
            {
                ID = 2,
            };

            session.Add("CurrentUser", user);

            using (var controller = new TwitchController(new TwitchIntegrationStub(), session))
            {
                var results = await controller.Authorize("code","scopes") as RedirectToRouteResult;

                string expectedController = "Account";
                string expectedAction = "EditIntegrations";

                Assert.AreEqual(expectedController, results.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, results.RouteValues["Action"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task DeAuthorize_SuccessfullDeAuthorizationReturnsCorrectAction_ExpectedActionEqualsActual()
        {
            var session = new SessionHelperFake();
            var user = new User
            {
                ID = 2,
            };

            session.Add("CurrentUser", user);

            using (var controller = new TwitchController(new TwitchIntegrationStub(), session))
            {
                var results = await controller.DeAuthorize() as RedirectToRouteResult;

                string expectedController = "Account";
                string expectedAction = "EditIntegrations";

                Assert.AreEqual(expectedController, results.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, results.RouteValues["Action"]);
            }
        }
    }
}
