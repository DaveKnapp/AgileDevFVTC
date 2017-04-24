using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Controllers;
using System.Web.Mvc;
using T5.Brothership.ViewModels;
using T5.Brothership.BL.Test.ManagerFakes;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;

namespace T5.Brothership.Web.Test.ControllersTest
{
    [TestClass]
    public class AccountControllerUnitTest
    {
        //public ActionResult EditIntegrations()
        //public ActionResult Update()
        //public async Task<ActionResult> Update(UpdateUserViewModel userViewModel)
        //public ActionResult ChangePassword()
        //public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)

        [TestMethod, TestCategory("UnitTest")]
        public void Create_ReturnCorrectView_ReturnsCreateView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                var result = controller.Create() as ViewResult;
                var expectedViewName = "Create";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task CreatePost_InvalidModelStateReturnCorrectView_ReturnsCreateView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var viewModel = new CreateUserViewModel
                {
                    CurrentUser = new User()
                };


                var result = await controller.Create(viewModel) as ViewResult;
                var expectedViewName = "Create";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task CreatePost_InvalidModelStateDisplayCorrectMessage_ExpectedViewBagMessageEqualActual()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var viewModel = new CreateUserViewModel
                {
                    CurrentUser = new User()
                };

                string expectedMessage = "An error occurred when creating the account.";

                var result = await controller.Create(viewModel) as ViewResult;

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task CreatePost_UserNameInUserReturnCorrectView_ReturnsCreateView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                var viewModel = new CreateUserViewModel
                {
                    CurrentUser = new User
                    {
                        UserName = "TestUserOne"
                    }
                };

                var result = await controller.Create(viewModel) as ViewResult;
                var expectedViewName = "Create";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task CreatePost_UserNameInUserDisplayCorrectMessage_ExpectedViewBagMessageEqualActual()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                var viewModel = new CreateUserViewModel
                {
                    CurrentUser = new User
                    {
                        UserName = "TestUserOne"
                    }
                };

                string expectedMessage = "Username is currently being used";

                var result = await controller.Create(viewModel) as ViewResult;

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task CreatePost_SuccessfullCreationRedirectsToCorrectView_ReturnsEditIntegraionView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {
                var viewModel = new CreateUserViewModel
                {
                    CurrentUser = new User
                    {
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
                    },
                    Password = "Password"
                };

                var result = await controller.Create(viewModel) as RedirectToRouteResult;
                var expectedViewName = "EditIntegrations";

                Assert.AreEqual(expectedViewName, result.RouteValues["Action"]);
            }
        }
    }
}
