using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using T5.Brothership.BL.Exceptions;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class UserMangerTest
    {
        [TestCategory("UnitTest"), TestMethod]
        public void GetById_UserFound_ReturnsCorrectUser()
        {
            var expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/Pofile.png",
                DateJoined = new DateTime(2017, 2, 23),
                DOB = new DateTime(1988, 11, 12),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "5Efg7nxAjJdkjIsZECyAWGA10mMixUnUiatbAgfcX3g=",
                    UserID = 1,
                    Salt = "b9qo1clGZ0q/99JkBJevOJGjU6JGUhmy"
                }
            };

            User actualUser;
            using (UserManager userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                actualUser = userManager.GetById(expectedUser.ID);
            }

            AssertUsersEqual(expectedUser, actualUser);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void GetById_UserNotFound_ReturnsNull()
        {
            User user;
            using (UserManager userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManager.GetById(99);
            }

            Assert.IsNull(user);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void Login_CorrectPassord_ReturnsUser()
        {
            User user;
            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManager.Login("TestUserOne", "Password");
            }

            Assert.IsNotNull(user);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void Login_InCorrect_ReturnsInvalidUser()
        {
            User user;

            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManager.Login("TestUserOne", "lkjasdlfjl");
            }

            Assert.IsTrue(user is InvalidUser);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void Login_InvalidUsername_ReturnsInvalidUser()
        {
            User user;

            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManager.Login("NotARealEmail@gmail.com", "Password");
            }

            Assert.IsTrue(user is InvalidUser);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void Login_InvalidEmail_ReturnsInvalidUser()
        {
            User user;

            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManager.Login("MrT", "Password");
            }

            Assert.IsTrue(user is InvalidUser);
        }

        [TestCategory("UnitTest"), TestMethod]
        public async Task Add_AllCorrectDataAdded_expectedDataEqualsActual()
        {
            var expectedPassword = "TestAddedUserPassord";
            var expectedUser = CreateExpectedAddedUser();
            User actualUser;

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (var userManager = new UserManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await userManager.Add(expectedUser, expectedPassword);
                    actualUser = fakeUnitOfWork.Users.GetByUsernameOrEmail(expectedUser.UserName);
                }
            }

            AssertUsersEqual(expectedUser, actualUser);
        }

        [TestCategory("UnitTest"), TestMethod,
         ExpectedExceptionAttribute(typeof(UsernameAlreadyExistsException))]
        public async Task Add_UsernameInUse_ThrowsUserNameAlreadyExistsException()
        {
            var expectedPassword = "TestAddedUserPassord";
            var expectedUser = CreateExpectedAddedUser();

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                using (var userManager = new UserManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await userManager.Add(expectedUser, expectedPassword);
                    await userManager.Add(expectedUser, expectedPassword);
                }
            }

        }

        [TestCategory("UnitTest"), TestMethod]
        public async Task Update_WasDataUpdated_ExpectedDataEqualsActual()
        {
            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork(), new GameApiServiceFake()))
            {
                var expectedUser = new User
                {
                    ID = 1,
                    UserName = "TestUserOne",
                    Bio = "UpdatedBio",
                    DateJoined = new DateTime(2017, 2, 23),
                    DOB = new DateTime(1999, 3, 22),
                    Email = "UserOneUpdatedEmail",
                    UserTypeID = 1,
                    GenderId = 2,
                    ProfileImagePath = "UpdatedImagePath.jpg",
                    Games = new List<Game>{ new Game {igdbID = 2909},
                                                     new Game {igdbID = 1035},
                                                     new Game {igdbID = 534},
                                                     new Game {igdbID = 1039}}
                };

                await userManager.Update(expectedUser);

                var actualUser = userManager.GetById(expectedUser.ID);

                AssertUsersEqual(expectedUser, actualUser);
                Assert.AreEqual(expectedUser.Games.Count(), actualUser.Games.Count());
            }
        }

        [TestCategory("UnitTest"), TestMethod]
        public void UserNameExists_UserExists_ReturnsTrue()
        {
            const string userName = "ThisNameIsInUse";

            using (var fakeUnitOfwork = new FakeBrothershipUnitOfWork())
            {
                fakeUnitOfwork.Users.Add(new User
                {
                    UserName = userName,
                    UserLogin = new UserLogin()
                });

                using (UserManager userManger = new UserManager(fakeUnitOfwork))
                {
                    Assert.IsTrue(userManger.UserNameExists(userName));
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UserNameExists_UserNotExists_ReturnsFalse()
        {
            const string userName = "ThisNameNotInUse";

            using (var fakeUnitOfwork = new FakeBrothershipUnitOfWork())
            {
                using (UserManager userManger = new UserManager(fakeUnitOfwork))
                {
                    Assert.IsFalse(userManger.UserNameExists(userName));
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void UpdatePassword_DoesPasswordChange_LoginSucessfulWithNewPassword()
        {
            const string userName = "TestUserOne";
            const string currentPassword = "Password";
            const string newPassword = "NewPassword";
            User user;

            using (UserManager userManger = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManger.Login(userName, currentPassword);

                userManger.UpdatePassword(currentPassword, newPassword, user);
                Assert.IsFalse(userManger.Login(userName, newPassword) is InvalidUser);
            }
        }


        [TestMethod, TestCategory("UnitTest"),
         ExpectedExceptionAttribute(typeof(InvalidPasswordException))]
        public void UpdatePassword_IncorrectCurrentPassword_ThrowsInvalidPasswordExcception()
        {
            const string userName = "TestUserOne";
            const string correctPassword = "Password";
            const string wrongPassword = "Wrong";
            const string newPassword = "NewPassword";
            User user;

            using (UserManager userManger = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                user = userManger.Login(userName, correctPassword);

                userManger.UpdatePassword(wrongPassword, newPassword, user);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetRandomPopularUsers_UsersReturned_CountGreaterThanOne()
        {
            using (UserManager userMangaer = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                int count = userMangaer.GetRandomPopularUsers(3, 10).Count;
                Assert.IsTrue(count > 1);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetRandomFeaturedUsers_UsersReturned_CountGreaterThanOne()
        {
            using (UserManager userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                int count = userManager.GetRandomFeaturedUsers(2).Count;
                Assert.IsTrue(count > 1);
            }
        }


        [TestMethod, TestCategory("UnitTest")]
        public void GetRandomFeaturedUsers_usersFromExcludedListAreExcluded_ListDoesNotContainUsersFromExcluded()
        {
            using (UserManager userMangaer = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                var excludedUsers = userMangaer.GetRandomPopularUsers(1, 3);
                var featuredUsers = userMangaer.GetRandomFeaturedUsers(2, excludedUsers);

                foreach (var user in featuredUsers)
                {
                    Assert.IsFalse(excludedUsers.Contains(user));
                }
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetRandomPopularUsers_usersFromExcludedListAreExcluded_ListDoesNotContainUsersFromExcluded()
        {
            using (UserManager userMangaer = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                var excludedUsers = userMangaer.GetRandomPopularUsers(2, 3);
                var popularUsers = userMangaer.GetRandomPopularUsers(2, 3, excludedUsers);

                foreach (var user in popularUsers)
                {
                    Assert.IsFalse(excludedUsers.Contains(user));
                }
            }
        }

        private User CreateExpectedAddedUser()
        {
            var expectedUser = new User
            {
                UserName = "AddedUserName",
                Email = "AddedUser@yahoo.com",
                Bio = "This is my bio.  I was just added",
                ProfileImagePath = "../Images/AddedUser/Pofile.png",
                DOB = new DateTime(1980, 2, 10),
                GenderId = 1,
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "5Efg7nxAjJdkjIsZECyAWGA10mMixUnUiatbAgfcX3g=",
                    Salt = "b9qo1clGZ0q/99JkBJevOJGjU6JGUhmy"
                }
            };

            expectedUser.Games.Add(new Game
            {
                igdbID = 18017
            });

            expectedUser.Games.Add(new Game
            {
                igdbID = 11194
            });

            expectedUser.Games.Add(new Game
            {
                igdbID = 2909
            });

            expectedUser.Games.Add(new Game
            {
                igdbID = 2276
            });

            expectedUser.Games.Add(new Game
            {
                igdbID = 534
            });
            return expectedUser;
        }

        private void AssertUsersEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Bio, actual.Bio);
            Assert.AreEqual(expected.DateJoined, actual.DateJoined);
            Assert.AreEqual(expected.DOB, actual.DOB);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
            Assert.AreEqual(actual.Games.Count, actual.Games.Count);

            foreach (var game in expected.Games)
            {
                Assert.IsNotNull(actual.Games.FirstOrDefault(p => p.igdbID == game.igdbID));
            }
        }
    }
}