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
            var userManager = new UserManagerFake
            {
                ValidPassword = "Password"
            };

            using (var controller = new AccountController(userManager,
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

            //public ActionResult EditIntegrations()
        }


        [TestMethod, TestCategory("UnitTest")]
        public void EditIntegrations_LoggedInUserReturnsCorrectView_ReturnsEditIntegraionView()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {

                var result = controller.EditIntegrations() as ViewResult;
                var expectedViewName = "EditIntegrations";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void EditIntegrations_LoggedOutUserReturnsCorrectView_ReturnsLoginView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {

                var result = controller.EditIntegrations() as RedirectToRouteResult;
                var expectedActionName = "Login";
                var expectedControllerName = "Login";

                Assert.AreEqual(expectedActionName, result.RouteValues["Action"]);
                Assert.AreEqual(expectedControllerName, result.RouteValues["Controller"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Update_LoggedInUserReturnsCorrectView_ReturnsUpdateView()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {

                var result = controller.Update() as ViewResult;
                var expectedViewName = "Update";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void Update_LoggedOutUserReturnsCorrectView_ReturnsLoginView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {

                var result = controller.EditIntegrations() as RedirectToRouteResult;
                var expectedActionName = "Login";
                var expectedControllerName = "Login";

                Assert.AreEqual(expectedActionName, result.RouteValues["Action"]);
                Assert.AreEqual(expectedControllerName, result.RouteValues["Controller"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdatePost_InvalidModelStateReturnCorrectView_ReturnsUpdateView()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var viewModel = new UpdateUserViewModel
                {
                    CurrentUser = new User()
                };


                var result = await controller.Update(viewModel) as ViewResult;
                var expectedViewName = "Update";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdatePost_InvalidModelStateReturnCorrectMessage_ActualMessageEqualsExpected()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var viewModel = new UpdateUserViewModel
                {
                    CurrentUser = new User()
                };


                var result = await controller.Update(viewModel) as ViewResult;

                var expectedMessage = "An error occurred when updating the account.";

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdatePost_SucessfullUpdateReturnsCorrectView_ReturnsUpdateView()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                var viewModel = new UpdateUserViewModel
                {
                    CurrentUser = new User()
                };


                var result = await controller.Update(viewModel) as ViewResult;
                var expectedViewName = "Update";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdatePost_SuccessfullUpdateReturnsCorrectMessage_ActualMessageEqualsExpected()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                var viewModel = new UpdateUserViewModel
                {
                    CurrentUser = new User()
                };


                var result = await controller.Update(viewModel) as ViewResult;

                var expectedMessage = "Account Successfully updated.";

                Assert.AreEqual(expectedMessage, result.ViewBag.UpdateMessage);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePassword_LoggedInUserReturnsCorrectView_ReturnsChangePassword()
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

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {

                var result = controller.ChangePassword() as ViewResult;
                var expectedViewName = "ChangePassword";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePassword_LoggedOutUserReturnsCorrectView_ReturnsLoginView()
        {
            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   new SessionHelperFake()))
            {

                var result = controller.ChangePassword() as RedirectToRouteResult;
                var expectedActionName = "Login";
                var expectedControllerName = "Login";

                Assert.AreEqual(expectedActionName, result.RouteValues["Action"]);
                Assert.AreEqual(expectedControllerName, result.RouteValues["Controller"]);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePasswordPost_InvalidModelReturnsCorrectView_ReturnsChangePasswordView()
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

            var viewModel = new ChangePasswordViewModel
            {
                UserName = "Bill",
                NewPassword = "billy",
                ConfirmPassword = "billy",
                CurrentPassword = "Password"
            };

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var result = controller.ChangePassword(viewModel) as ViewResult;
                var expectedViewName = "ChangePassword";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }


        [TestMethod, TestCategory("UnitTest")]
        public void ChangePasswordPost_InvalidModelReturnsCorrectMessage_ExpectedMessageEqualsActual()
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

            var viewModel = new ChangePasswordViewModel
            {
                UserName = "Bill",
                NewPassword = "billy",
                ConfirmPassword = "billy",
                CurrentPassword = "Password"
            };

            using (var controller = new AccountController(new UserManagerFake(),
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                controller.ModelState.AddModelError("SessionName", "Required");
                var result = controller.ChangePassword(viewModel) as ViewResult;
                var expectedMessage = "An error occurred when updating your password.";

                Assert.AreEqual(expectedMessage, result.ViewBag.Message);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePasswordPost_ChangedSuccessfullReturnsCorrectView_ExpectedViewEqualsActual()
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

            var viewModel = new ChangePasswordViewModel
            {
                UserName = "Bill",
                NewPassword = "billy",
                ConfirmPassword = "billy",
                CurrentPassword = "Password"
            };

            var userManager = new UserManagerFake { ValidPassword = "Password" };

            using (var controller = new AccountController(userManager,
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                var result = controller.ChangePassword(viewModel) as ViewResult;
                var expectedView = "PasswordUpdated";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePasswordPost_InvalidPasswordReturnsCorrectView_ExpectedViewEqualsActual()
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

            var viewModel = new ChangePasswordViewModel
            {
                UserName = "Bill",
                NewPassword = "billy",
                ConfirmPassword = "billy",
                CurrentPassword = "Password"
            };

            var userManager = new UserManagerFake { ValidPassword = "Invalid" };

            using (var controller = new AccountController(userManager,
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                var result = controller.ChangePassword(viewModel) as ViewResult;
                var expectedView = "ChangePassword";

                Assert.AreEqual(expectedView, result.ViewName);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ChangePasswordPost_InvalidPasswordReturnsCorrectMessage_ExpectedMessageEqualsActual()
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

            var viewModel = new ChangePasswordViewModel
            {
                UserName = "Bill",
                NewPassword = "billy",
                ConfirmPassword = "billy",
                CurrentPassword = "Password"
            };

            var userManager = new UserManagerFake { ValidPassword = "Invalid" };

            using (var controller = new AccountController(userManager,
                                                   new NationalityManagerFake(),
                                                   new GenderManagerFake(),
                                                   session))
            {
                var result = controller.ChangePassword(viewModel) as ViewResult;
                var expectedView = "Invalid Password";

                Assert.AreEqual(expectedView, result.ViewBag.Message);
            }
        }
        //TOOD Post Model valid
    }
}
