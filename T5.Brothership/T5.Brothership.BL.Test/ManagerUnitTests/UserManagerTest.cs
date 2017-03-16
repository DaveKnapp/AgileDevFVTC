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

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    //TODO Add game Manager integtration test.
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

        [TestCategory("UnitTest"), TestMethod]
        public async Task Update_WasDataUpdated_ExpectedDataEqualsActual()
        {
            using (var userManager = new UserManager(new FakeBrothershipUnitOfWork(), new GameApiServiceFake()))
            {
                const int userId = 1;
                var expectedUser = new User();

                expectedUser.Bio = "UpdatedBio";
                expectedUser.DateJoined = DateTime.Now;
                expectedUser.DOB = new DateTime(1999, 3, 22);
                expectedUser.Email = "UserOneUpdatedEmail";
                expectedUser.GenderId = 2;
                expectedUser.ProfileImagePath = "UpdatedImagePath.jpg";
                expectedUser.Games = new List<Game>{ new Game {igdbID = 4325},
                                                     new Game {igdbID = 5314},
                                                     new Game {igdbID = 2276},
                                                     new Game {igdbID = 1039}};

                userManager.Update(expectedUser);

                var actualUser = userManager.GetById(userId);

                AssertUsersEqual(expectedUser, actualUser);
                Assert.AreEqual(expectedUser.Games.Count(), actualUser.Games.Count());

                foreach (var game in expectedUser.Games)
                {
                    Assert.IsTrue(actualUser.Games.Contains(game));
                }
            }
        }

        private static User CreateExpectedAddedUser()
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
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
            Assert.AreEqual(expected.UserLogin.UserID, actual.UserLogin.UserID);
            Assert.AreEqual(expected.UserLogin.PasswordHash, actual.UserLogin.PasswordHash);
            Assert.AreEqual(actual.Games.Count, actual.Games.Count);

            foreach (var game in expected.Games)
            {
                Assert.IsNotNull(actual.Games.Where(p => p.igdbID == game.igdbID));
            }
        }
    }
}