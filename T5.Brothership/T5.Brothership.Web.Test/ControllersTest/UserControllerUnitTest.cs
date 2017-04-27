using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using T5.Brothership.Controllers;
using T5.Brothership.BL.Test.IntegrationFakes;
using T5.Brothership.BL.Test.ManagerFakes;
using System.Web.Mvc;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;

namespace T5.Brothership.Web.Test.ControllersTest
{
    [TestClass]
    public class UserControllerUnitTest
    {

        //public ActionResult UserGames(string userName)
        //public ActionResult UserRatings(string userName)
        //public ActionResult Rate(string userName)
        [TestMethod, TestCategory("UnitTest")]
        public async Task User_ValidUserNameReturnsCorrectView_ReturnedViewEqualsUser()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = await controller.User("TestUserOne") as ViewResult;

                string expectedView = "User";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task User_InvalidValidUserNameThrowsHttpException_ResultEqualsHttpException()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = await controller.User("Fail") as HttpNotFoundResult;

                Assert.IsNotNull(result);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UserGames_ValidUserNameReturnsCorrectView_ReturnedViewEqualsUserGames()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.UserGames("TestUserOne") as ViewResult;

                string expectedView = "UserGames";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UserGames_InvalidValidUserNameThrowsHttpException_ResultEqualsHttpException()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.UserGames("Fail") as HttpNotFoundResult;

                Assert.IsNotNull(result);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UserRatings_ValidUserNameReturnsCorrectView_ReturnedViewEqualsUserRatings()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.UserRatings("TestUserOne") as ViewResult;

                string expectedView = "UserRatings";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UserRatings_InvalidValidUserNameThrowsHttpException_ResultEqualsHttpException()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.UserRatings("Fail") as HttpNotFoundResult;

                Assert.IsNotNull(result);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Rate_LoggedInUserAndValidUserNameReturnsCorrectView_ReturnedViewEqualsRating()
        {
            var session = new SessionHelperFake();

            var user = new User
            {
                ID = 10
            };

            session.Add("CurrentUser", user);

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      session))
            {
                var result = controller.Rate("TestUserOne") as ViewResult;

                string expectedView = "Rate";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Rate_LoggedOutUserReturnsCorrectView_ReturnedViewEqualsLogin()
        {
            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.Rate("TestUserOne") as RedirectToRouteResult;

                string expectedController = "Login";
                string expectedAction = "Login";

                Assert.AreEqual(expectedController, result.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, result.RouteValues["Action"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Rate_LoggedInUserAndInvValidUserNameReturnsException_ReturnedHttpNotForException()
        {
            var session = new SessionHelperFake();

            var user = new User
            {
                ID = 10
            };

            session.Add("CurrentUser", user);

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      session))
            {
                var result = controller.Rate("Fail") as HttpNotFoundResult;

                Assert.IsNotNull(result);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void RatePost_RatingSuccessfullReturnsCorrectView_ReturnedViewEqualsUser()
        {
            var session = new SessionHelperFake();

            var user = new User
            {
                ID = 1
            };

            session.Add("CurrentUser", user);

            var viewModel = new UserRatingViewModel
            {
                UserRating = new UserRating
                {
                    UserBeingRatedID = 3,
                    Comment = "Nice",
                    RaterUserID = 1
                }
            };

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      session))
            {
                var result = controller.Rate(viewModel) as RedirectToRouteResult;

                string expectedController = "User";
                string expectedAction = "User";

                Assert.AreEqual(expectedController, result.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, result.RouteValues["Action"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void RatePost_UserNotLoggedInReturnsCorrectView_ReturnedViewEqualsLogin()
        {
            var viewModel = new UserRatingViewModel
            {
                UserRating = new UserRating
                {
                    UserBeingRatedID = 3,
                    Comment = "Nice",
                    RaterUserID = 1
                }
            };

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      new SessionHelperFake()))
            {
                var result = controller.Rate(viewModel) as RedirectToRouteResult;

                string expectedController = "Login";
                string expectedAction = "Login";

                Assert.AreEqual(expectedController, result.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, result.RouteValues["Action"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void RatePost_InvalidModelStateReturnsCorrectView_ReturnedViewEqualsRate()
        {
            var session = new SessionHelperFake();

            var user = new User
            {
                ID = 1
            };

            session.Add("CurrentUser", user);

            var viewModel = new UserRatingViewModel
            {
                UserRating = new UserRating
                {
                    UserBeingRatedID = 3,
                    Comment = "Nice",
                    RaterUserID = 1
                }
            };

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var result = controller.Rate(viewModel) as ViewResult;

                string expectedView = "Rate";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void RatePost_InvalidModelStateReturnsCorrectMessage_ExpectedMessageEqualsActual()
        {
            var session = new SessionHelperFake();

            var user = new User
            {
                ID = 1
            };

            session.Add("CurrentUser", user);

            var viewModel = new UserRatingViewModel
            {
                UserRating = new UserRating
                {
                    UserBeingRatedID = 3,
                    Comment = "Nice",
                    RaterUserID = 1
                }
            };

            using (UserController controller = new UserController(new TwitchIntegrationStub(),
                                                                      new TwitterIntegrationStub(),
                                                                      new UserManagerFake(),
                                                                      new UserRatingManagerFake(),
                                                                      new RatingManagerFake(),
                                                                      session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var result = controller.Rate(viewModel) as ViewResult;

                string expectedMessage = "An error occurred when submitting rating.";

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }
    }
}
