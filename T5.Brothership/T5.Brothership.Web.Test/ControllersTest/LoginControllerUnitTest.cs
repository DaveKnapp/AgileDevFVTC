using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.Controllers;
using T5.Brothership.BL.Test.ManagerFakes;
using System.Web.Mvc;
using T5.Brothership.ViewModels;

namespace T5.Brothership.Web.Test.ControllersTest
{
    [TestClass]
    public class LoginControllerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void Login_LoggedInUserReturnsCorrectView_ViewNameEqualsLogin()
        {
            var session = new SessionHelperFake();
            var user = new User
            {
                ID = 1,
                UserName = "MrBlack",
                Email = "TestingUser6@yahoo.com",
                Bio = "Hello I am the Sixth test user",
                ProfileImagePath = "../Images/TestUserSix/Profile.png",
                DateJoined = new DateTime(2017, 1, 14),
                DOB = new DateTime(1955, 5, 7),
                GenderId = 1,
                UserType = new UserType { ID = 2, Description = "FeaturedUser" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
            };

            session.Add("CurrentUser", user);

            using (var controller = new LoginController(new UserManagerFake(), session))
            {
                var result = controller.Login() as RedirectToRouteResult;
                string expectedController = "Home";
                string expectedAction = "Index";

                Assert.AreEqual(expectedController, result.RouteValues["Controller"]);
                Assert.AreEqual(expectedAction, result.RouteValues["Action"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Login_LoggedOutUserReturnsCorrectView_ViewNameEqualsLogin()
        {
            using (var controller = new LoginController(new UserManagerFake(), new SessionHelperFake()))
            {
                var result = controller.Login() as ViewResult;
                string expectedView = "Login";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void LoginPost_SuccessfullLoginReturnsCorrectView_ViewNameEqualIndex()
        {
            var userManager = new UserManagerFake { ValidPassword = "Password" };

            var model = new LoginModel
            {
                Username = "bob",
                Password = "Password"
            };

            using (var controller = new LoginController(userManager, new SessionHelperFake()))
            {
                var result = controller.Login(model) as RedirectToRouteResult;
                string expectedAction = "Index";
                string expectedController = "Home";

                Assert.AreEqual(expectedAction, result.RouteValues["Action"]);
                Assert.AreEqual(expectedController, result.RouteValues["Controller"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void LoginPost_UnsuccessfullLoginReturnsCorrectView_ViewNameEqualsLogin()
        {
            var model = new LoginModel
            {
                Username = "Bob",
                Password = "Fail"
            };

            using (var controller = new LoginController(new UserManagerFake(), new SessionHelperFake()))
            {
                var result = controller.Login() as ViewResult;
                string expectedView = "Login";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void LoginPost_UnsuccessfullLoginReturnsMessage_ExpectedMessageEqualsActualMessage()
        {
            var model = new LoginModel
            {
                Username = "Bob",
                Password = "Fail"
            };

            using (var controller = new LoginController(new UserManagerFake(), new SessionHelperFake()))
            {
                var result = controller.Login(model) as ViewResult;
                string expectedMessage = "Invalid Username or Password";

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Logout_SuccessfullLogoutReturnsCorrectView_ViewNameEqualsLogin()
        {
            using (var controller = new LoginController(new UserManagerFake(), new SessionHelperFake()))
            {
                var result = controller.LogOut() as ViewResult;
                string expectedView = "Login";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }
    }
}
